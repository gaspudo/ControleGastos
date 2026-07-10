using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleGastos.Domain.Exceptions
{
    public class RegraDeNegocioException : AppException
    {
        public RegraDeNegocioException(string message) : base(message) { }
        public override int StatusCode => 400; // Bad Request
    }
}