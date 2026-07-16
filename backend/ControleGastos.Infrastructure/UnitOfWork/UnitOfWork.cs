using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControleGastos.Domain.Interfaces;
using ControleGastos.Infrastructure.Data;
using Microsoft.EntityFrameworkCore.Storage;

namespace ControleGastos.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {

        public IPessoaRepository Pessoas {get; }
        private readonly Context _context;
        public ITransacaoRepository Transacoes {get;}
        private IDbContextTransaction? _transaction;

        public UnitOfWork (IPessoaRepository pessoaRepository, Context contexto, ITransacaoRepository transacao)
        {
            Pessoas = pessoaRepository;
            _context = contexto;
            Transacoes = transacao;
        }

        public async Task BeginTransactionAsync()
        {
            // impede abrir uma nova transação se já houver uma ativa no mesmo escopo
            if (_transaction != null)
            {
                throw new InvalidOperationException("Uma transação já está em andamento neste Unit of Work.");
            }

            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitAsync()
        {
            if (_transaction == null)
            {
                throw new InvalidOperationException("Não é possível commitar: nenhuma transação foi iniciada.");
            }

            try
            {
                // Persiste as alterações pendentes antes do Commit do banco
                await _context.SaveChangesAsync();
                await _transaction.CommitAsync();
            }
            catch
            {
                // Se o Commit ou SaveChanges falhar garante o Rollback para proteger a integridade
                await RollbackAsync();
                throw;
            }
            finally
            {
                DisposeTransaction();
            }
        }

        public async Task RollbackAsync()
        {
            if (_transaction == null) return;

            try
            {
                await _transaction.RollbackAsync();
            }
            finally
            {
                DisposeTransaction();
            }
        }

        private void DisposeTransaction()
        {
            if (_transaction != null)
            {
                _transaction.Dispose();
                _transaction = null;
            }
        }
    }
}