using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using PruebaTecnica.Model.Base;

namespace PruebaTecnica.Model.ModelUser
{
    public class AppUser : BaseEntity
    {
        [Column(TypeName = "VARCHAR"), StringLength(64)]
        public string Name { get; set; }

        [Column(TypeName = "VARCHAR"), StringLength(128)]
        public string Password { get; set; }

        public int RoleId { get; set; }
        public virtual Role Role { get; set; }
    }
}
