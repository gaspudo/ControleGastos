using ControleGastos.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ControleGastos.Infrastructure.Data
{
    public class Context : DbContext
    {
        public DbSet<Pessoa> Pessoas {get;set;}
        public DbSet<Transacao> Transacoes {get;set;}

        public Context (DbContextOptions<Context> options) : base(options) {}
    
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<Transacao>()
            .HasOne<Pessoa>()
            .WithMany()
            .HasForeignKey(t => t.PessoaId)
            .OnDelete(DeleteBehavior.Restrict);
        }
    }
}