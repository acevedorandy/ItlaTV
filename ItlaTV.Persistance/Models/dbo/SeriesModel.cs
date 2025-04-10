

namespace ItlaTV.Persistance.Models.dbo
{
    public class SeriesModel
    {
        public int SerieID { get; set; }
        public string Nombre { get; set; }
        public string ImagenPortada { get; set; }
        public string EnlaceVideo { get; set; }
        public int ProductoraID { get; set; }
        public int? GeneroPrimarioID { get; set; }
        public int? GeneroSecundarioID { get; set; }
    }
}
