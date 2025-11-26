using PruebaTecnica.Model.BaseDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnica.Model.DTO
{
    public  class PersonDTO: BaseEntityDTO<int>
    {
        //Person
        public string PersonName { get; set; }
        public string PersonLastName { get; set; }
        public string PersonDNI { get; set; }
        public DateTime PersonBirthDate { get; set; }
        public string PersonPhone { get; set; }
        public string PersonEmail { get; set; }
        public int PersonAge { get; set; }

    }
}
