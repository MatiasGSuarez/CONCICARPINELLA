using PruebaTecnica.Model.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnica.Model.ModelUser
{
    public class Role : BaseEntity
    {
        [Column(TypeName = "VARCHAR"), StringLength(64)]
        public string Name { get; set; }

        [Column(TypeName = "VARCHAR"), StringLength(256)]
        public string Permissions { get; set; }

        public virtual ICollection<AppUser> AppUsers { get; set; }
    }

}
