

using ItlaTV.Domain.Entities.dbo;
using ItlaTV.Domain.Result;

namespace ItlaTV.Persistance.Validations.dbo
{
    public class GenerosValidation
    {
        public OperationResult ValidateGenerosSave(Generos generos, OperationResult result)
        {
            if (generos == null)
            {
                result.Success = false;
                result.Message = "Ha ocurrido un error guardando el genero.";
                return result;
            }
            if (string.IsNullOrEmpty(generos.Nombre) || generos.Nombre.Length > 255)
            {
                result.Success = false;
                result.Message = "El nombre del genero es requerido y debe ser menor a 255 caracteres.";
                return result;
            }
            return result;
        }
        public OperationResult ValidateGenerosUpdate(Generos generos, OperationResult result)
        {
            if (generos == null)
            {
                result.Success = false;
                result.Message = "El genero es requerido para actualizar.";
                return result;
            }
            if (string.IsNullOrEmpty(generos.Nombre) || generos.Nombre.Length > 255)
            {
                result.Success = false;
                result.Message = "El nombre del genero es requerido y debe ser menor a 255 caracteres.";
                return result;
            }
            return result;
        }
        public OperationResult ValidateGenerosRemove(Generos generos, OperationResult result)
        {
            if (generos == null)
            {
                result.Success = false;
                result.Message = "El genero es requerido para eliminar.";
                return result;
            }
            if (generos.GeneroID <= 0)
            {
                result.Success = false;
                result.Message = "El ID del genero es requerido para eliminar.";
                return result;
            }
            return result;
        }
    }
}
