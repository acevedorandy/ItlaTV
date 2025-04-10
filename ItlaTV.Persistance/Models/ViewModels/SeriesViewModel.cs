

using System.ComponentModel.DataAnnotations;

namespace ItlaTV.Persistance.Models.ViewModels
{
    public class SeriesViewModel
    {
        public int SerieID { get; set; }
        public string Nombre { get; set; }

        [Display(Name = "Portada")]
        public string ImagenPortada { get; set; }

        [Display(Name = "Productora")]
        public string NombreProductora { get; set; }

        [Display(Name = "Género Primario")]
        public string GeneroPrimario { get; set; }

        [Display(Name = "Género Secundario")]
        public string GeneroSecundario { get; set; }
        public string EnlaceVideo { get; set; }

    }
}
