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
    public class GenerosRepository(ItlaTVContext itlaTVContext,
                 ILogger<GenerosRepository> logger, GenerosValidation generosValidation) : BaseRepository<Generos>(itlaTVContext), IGenerosRepository
    {
        private readonly ItlaTVContext _itlaTVContext = itlaTVContext;
        private readonly ILogger<GenerosRepository> _logger = logger;
        private readonly GenerosValidation _generosValidation = generosValidation;

        public async override Task<OperationResult> Save(Generos generos)
        {
            OperationResult result = new OperationResult();

            _generosValidation.ValidateGenerosSave(generos, result);

            try
            {
                result = await base.Save(generos);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Hubo un error guardando el genero.";
                _logger.LogError(result.Message, ex.ToString());
            }
            return result;
        }

        public async override Task<OperationResult> Update(Generos generos)
        {
            OperationResult result = new OperationResult();

            _generosValidation.ValidateGenerosUpdate(generos, result);

            try
            {
                Generos? generoToUpdate = await _itlaTVContext.Generos.FindAsync(generos.GeneroID);

                generoToUpdate.GeneroID = generos.GeneroID;
                generoToUpdate.Nombre = generos.Nombre;

                result = await base.Update(generoToUpdate);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Ha ocurrido un error actualizando el genero.";
                _logger.LogError(result.Message, ex.ToString()); 
            }
            return result;
        }

        public async override Task<OperationResult> Remove(Generos generos)
        {
            OperationResult result = new OperationResult();

            _generosValidation.ValidateGenerosRemove(generos, result);

            try
            { 
                result = await base.Remove(generos);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Ha ocurrido un error eliminando el genero.";
                _logger.LogError(result.Message, ex.ToString());
            }
            return result;
        }

        public async override Task<OperationResult> GetAll()
        {
            OperationResult result = new OperationResult();

            try
            {
                result.Data = await (from db in _itlaTVContext.Generos
                                     orderby db.GeneroID descending

                                     select new GenerosModel()
                                     {
                                         GeneroID = db.GeneroID,
                                         Nombre = db.Nombre

                                     }).AsNoTracking()
                                     .ToListAsync();
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Hubo un error obteniendo todos los generos.";
                _logger.LogError(result.Message, ex.ToString());
            }
            return result;
        }

        public async override Task<OperationResult> GetById(int id)
        {
            OperationResult result = new OperationResult();

            try
            {
                result.Data = await (from db in _itlaTVContext.Generos
                                     where db.GeneroID == id

                                     select new GenerosModel()
                                     {
                                         GeneroID = db.GeneroID,
                                         Nombre = db.Nombre

                                     }).AsNoTracking()
                                     .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Hubo un error obteniendo el genero.";
                _logger.LogError(result.Message, ex.ToString());
            }
            return result;
        }
    }
}
