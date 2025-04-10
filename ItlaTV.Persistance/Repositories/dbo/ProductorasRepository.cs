

using ItlaTV.Domain.Entities.dbo;
using ItlaTV.Domain.Result;
using ItlaTV.Persistance.Base;
using ItlaTV.Persistance.Context;
using ItlaTV.Persistance.Interfaces.dbo;
using ItlaTV.Persistance.Models.dbo;
using ItlaTV.Persistance.Validations.dbo;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ItlaTV.Persistance.Repositories.dbo
{
    public sealed class ProductorasRepository(ItlaTVContext itlaTVContext,
                        ILogger<ProductorasRepository> logger, ProductorasValidation productorasValidation) : BaseRepository<Productoras>(itlaTVContext), IProductorasRepository
    {
        private readonly ItlaTVContext _itlaTVContext = itlaTVContext;
        private readonly ILogger<ProductorasRepository> _logger = logger;
        private readonly ProductorasValidation _productorasValidation = productorasValidation;

        public async override Task<OperationResult> Save(Productoras productoras)
        {
            OperationResult result = new OperationResult();

            _productorasValidation.ValidateProductoraSave(productoras, result);

            try
            {
                result = await base.Save(productoras);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Hubo un error guardando el genero.";
                _logger.LogError(result.Message, ex.ToString());
            }
            return result;
        }

        public async override Task<OperationResult> Update(Productoras productoras)
        {
            OperationResult result = new OperationResult();

            _productorasValidation.ValidateProductoraUpdate(productoras, result);

            try
            {
                Productoras? productoraToUpdate = await _itlaTVContext.Productoras.FindAsync(productoras.ProductoraID);

                productoraToUpdate.ProductoraID = productoras.ProductoraID;
                productoraToUpdate.Nombre = productoras.Nombre;
                productoraToUpdate.LogoURL = productoras.LogoURL;

                result = await base.Update(productoraToUpdate);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Ha ocurrido un error actualizando la productora.";
                _logger.LogError(result.Message, ex.ToString());
            }
            return result;
        }

        public async override Task<OperationResult> Remove(Productoras productoras)
        {
            OperationResult result = new OperationResult();

            _productorasValidation.ValidateProductoraRemove(productoras, result);

            try
            {
                result = await base.Remove(productoras);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Ha ocurrido un error eliminando la productora.";
                _logger.LogError(result.Message, ex.ToString());
            }
            return result;
        }

        public async override Task<OperationResult> GetAll()
        {
            OperationResult result = new OperationResult();

            try
            {
                result.Data = await (from dbo in _itlaTVContext.Productoras
                                     orderby dbo.ProductoraID descending

                                     select new ProductorasModel()
                                     { 
                                         ProductoraID = dbo.ProductoraID,
                                         Nombre = dbo.Nombre,
                                         LogoURL = dbo.LogoURL

                                     }).AsNoTracking()
                                     .ToListAsync();
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Hubo un error obteniendo las productoras.";
                _logger.LogError (result.Message, ex.ToString());
            }
            return result;
        }

        public async override Task<OperationResult> GetById(int id)
        {
            OperationResult result = new OperationResult();

            try
            {
                result.Data = await (from dbo in _itlaTVContext.Productoras
                                     where dbo.ProductoraID == id 

                                     select new ProductorasModel()
                                     {
                                         ProductoraID = dbo.ProductoraID,
                                         Nombre = dbo.Nombre,
                                         LogoURL = dbo.LogoURL

                                     }).AsNoTracking()
                                     .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Hubo un error obteniendo la productora.";
                _logger.LogError(result.Message, ex.ToString());
            }
            return result;
        }
    }
}
