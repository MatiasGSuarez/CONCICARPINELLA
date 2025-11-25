using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnica.Model.Base
{
    public class PagingSortFilterRequest
    {
        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public string FilterBy { get; set; }

        public string FilterValue { get; set; }

        public string OrderBy { get; set; }

        public bool OnlyEnabled { get; set; }

        public PagingSortFilterRequest()
        {
            OnlyEnabled = true;
        }
    }
}