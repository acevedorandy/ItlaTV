using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ItlaTV.Domain.Entities.dbo
{
    [Table("Productoras", Schema = "dbo")]

    public class Productoras
    {
        [Key]
        public int ProductoraID { get; set; }
        public string Nombre { get; set; }
        public string? LogoURL { get; set; }
    }
}
