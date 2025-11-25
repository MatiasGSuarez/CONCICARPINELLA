using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnica.Model.BaseDTO
{
    public class ExceptionDTO
    {
        public string Message { get; set; }

        public string LogId { get; set; }

        public string StackTrace { get; set; }
    }
}