using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace PruebaTecnica.Model.Base
{
    public abstract class BaseEntity : IEntity, ISoftDelete
    {
        public int Id { get; set; }

        [Column(TypeName = "VARCHAR"), StringLength(64)]
        public string CreatedBy { get; set; }

        [Column(TypeName = "VARCHAR"), StringLength(64)]
        public string UpdatedBy { get; set; } = string.Empty; // Valor por defecto

        [Column(TypeName = "VARCHAR"), StringLength(64)]
        public string DeletedBy { get; set; } = string.Empty; // Valor por defecto

        public DateTime? Created { get; set; }
        public DateTime? Updated { get; set; }
        public DateTime? Deleted { get; set; }

        public bool IsNew
        {
            get
            {
                return Id.Equals(0);
            }
        }
    }
}