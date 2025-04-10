

using ItlaTV.Domain.Entities.dbo;
using ItlaTV.Domain.Result;

namespace ItlaTV.Persistance.Validations.dbo
{
    public class ProductorasValidation
    {
        public OperationResult ValidateProductoraSave(Productoras productoras, OperationResult result)
        {
            if (productoras == null)
            {
                result.Success = false;
                result.Message = "La productora es requerida.";
                return result;
            }
            if (string.IsNullOrEmpty(productoras.Nombre) || productoras.Nombre.Length > 255)
            {
                result.Success = false;
                result.Message = "El nombre de la productora es requerido y debe ser menor a 255 caracteres.";
                return result;
            }
            return result;
        }

        public OperationResult ValidateProductoraUpdate(Productoras productoras, OperationResult result)
        {
            if (productoras == null)
            {
                result.Success = false;
                result.Message = "La productora es requerida.";
                return result;
            }
            if (string.IsNullOrEmpty(productoras.Nombre) || productoras.Nombre.Length > 255)
            {
                result.Success = false;
                result.Message = "El nombre de la productora es requerido y debe ser menor a 255 caracteres.";
                return result;
            }
            return result;
        }

        public OperationResult ValidateProductoraRemove(Productoras productoras, OperationResult result)
        {
            if (productoras == null)
            {
                result.Success = false;
                result.Message = "La productora es requerida.";
                return result;
            }
            if (productoras.ProductoraID <= 0)
            {
                result.Success = false;
                result.Message = "El ID de la productora es requerido.";
                return result;
            }
            return result;
        }
    }
}
