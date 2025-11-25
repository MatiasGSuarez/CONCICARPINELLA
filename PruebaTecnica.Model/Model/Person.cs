using PruebaTecnica.Model.Base;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PruebaTecnica.Model.Model
{
    public abstract class Person : BaseEntity
    {
        public string Nombre { get; set; }

    }
}

