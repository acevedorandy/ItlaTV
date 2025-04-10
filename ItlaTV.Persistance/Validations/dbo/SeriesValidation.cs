

using ItlaTV.Domain.Entities.dbo;
using ItlaTV.Domain.Result;

namespace ItlaTV.Persistance.Validations.dbo
{
    public class SeriesValidation
    {
        public OperationResult ValidateSeriesSave(Series series, OperationResult result) 
        {
            if (series == null)
            {
                result.Success = false;
                result.Message = "La serie es requerida.";
                return result;
            }
            if (string.IsNullOrEmpty(series.Nombre) || series.Nombre.Length > 255)
            {
                result.Success = false;
                result.Message = "El nombre de la serie es requerido y debe ser menor a 255 caracteres.";
                return result;
            }
            if (series.ImagenPortada == null)
            {
                result.Success = false;
                result.Message = "La imagen de portada es requerida.";
            }
            if (series.EnlaceVideo == null)
            {
                result.Success = false;
                result.Message = "El enlace del video es requerido.";
                return result;
            }
            if (series.ProductoraID <= 0)
            {
                result.Success = false;
                result.Message = "El ID de la productora es requerido.";
                return result;
            }
            if (series.GeneroPrimarioID <= 0)
            {
                result.Success = false;
                result.Message = "La ID del genero primario primario es requerido.";
                return result;
            }
            return result;
        }

        public OperationResult ValidateSeriesUpdate(Series series, OperationResult result)
        {
            if (series == null)
            {
                result.Success = false;
                result.Message = "La serie es requerida.";
                return result;
            }
            if (string.IsNullOrEmpty(series.Nombre) || series.Nombre.Length > 255)
            {
                result.Success = false;
                result.Message = "El nombre de la serie es requerido y debe ser menor a 255 caracteres.";
                return result;
            }
            if (series.ImagenPortada == null)
            {
                result.Success = false;
                result.Message = "La imagen de portada es requerida.";
            }
            if (series.EnlaceVideo == null)
            {
                result.Success = false;
                result.Message = "El enlace del video es requerido.";
                return result;
            }
            if (series.ProductoraID <= 0)
            {
                result.Success = false;
                result.Message = "El ID de la productora es requerido.";
                return result;
            }
            if (series.GeneroPrimarioID <= 0)
            {
                result.Success = false;
                result.Message = "La ID del genero primario primario es requerido.";
                return result;
            }
            return result;
        }

        public OperationResult ValidateSeriesRemove(Series series, OperationResult result)
        {
            if (series == null)
            {
                result.Success = false;
                result.Message = "La serie es requerida.";
                return result;
            }
            if (series.SerieID <= 0)
            {
                result.Success = false;
                result.Message = "El ID de la serie es requerido.";
                return result;
            }
            return result;
        }
    }
}
