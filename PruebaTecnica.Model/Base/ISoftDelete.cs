using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnica.Model.Base
{
    public interface ISoftDelete
    {
        public DateTime? Deleted { get; set; }
    }
}
