using ItlaTV.Domain.Entities.dbo;
using ItlaTV.Domain.Result;
using ItlaTV.Persistance.Base;
using ItlaTV.Persistance.Context;
using ItlaTV.Persistance.Interfaces.dbo;
using ItlaTV.Persistance.Models.ViewModels;
using ItlaTV.Persistance.Validations.dbo;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ItlaTV.Persistance.Repositories.dbo
{
    public sealed class SeriesRepository(ItlaTVContext itlaTVContext,
                        ILogger<SeriesRepository> logger, SeriesValidation seriesValidation) : BaseRepository<Series>(itlaTVContext), ISeriesRepository
    {
        private readonly ItlaTVContext _itlaTVContext = itlaTVContext;
        private readonly ILogger<SeriesRepository> _logger = logger;
        private readonly SeriesValidation _seriesValidation = seriesValidation;

        public async override Task<OperationResult> Save(Series series)
        {
            OperationResult result = new OperationResult();

            _seriesValidation.ValidateSeriesSave(series, result);

            try
            {
                result = await base.Save(series);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Ha ocurrido un error guardando la serie.";
                _logger.LogError(result.Message, ex.ToString());
            }
            return result;
        }

        public async override Task<OperationResult> Update(Series series)
        {
            OperationResult result = new OperationResult();

            _seriesValidation.ValidateSeriesUpdate(series, result);

            try
            {
                Series? seriesToUpdate = await _itlaTVContext.Series.FindAsync(series.SerieID);

                seriesToUpdate.SerieID = series.SerieID;
                seriesToUpdate.Nombre = series.Nombre;
                seriesToUpdate.ImagenPortada = series.ImagenPortada;
                seriesToUpdate.EnlaceVideo = series.EnlaceVideo;
                seriesToUpdate.ProductoraID = series.ProductoraID;
                seriesToUpdate.GeneroPrimarioID = series.GeneroPrimarioID;
                seriesToUpdate.GeneroSecundarioID = series.GeneroSecundarioID;

                result = await base.Update(seriesToUpdate);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Ha ocurrido un error actualizando la serie.";
                _logger.LogError(result.Message, ex.ToString());    
            }
            return result;
        }

        public async override Task<OperationResult> Remove(Series series)
        {
            OperationResult result = new OperationResult();

            _seriesValidation.ValidateSeriesRemove(series, result);

            try
            {
                result = await base.Remove(series);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Hubo un error eliminando la serie.";
                _logger.LogError(result.Message, ex.ToString());
            }
            return result;
        }

        // Este metodo de momento no se utiliza con la entidad Series
        public async override Task<OperationResult> GetAll()
        {
            OperationResult result = new OperationResult();

            try
            {
                result.Data = await (from dbo in _itlaTVContext.Series
                                     join dbo1 in _itlaTVContext.Productoras on dbo.ProductoraID equals dbo1.ProductoraID
                                     join dbo2 in _itlaTVContext.Generos on dbo.GeneroPrimarioID equals dbo2.GeneroID into generoPrimario
                                     from dbo2 in generoPrimario.DefaultIfEmpty()
                                     join dbo3 in _itlaTVContext.Generos on dbo.GeneroSecundarioID equals dbo3.GeneroID into generoSecundario
                                     from dbo3 in generoSecundario.DefaultIfEmpty()

                                     orderby dbo.SerieID descending

                                     select new SeriesViewModel()
                                     {
                                         SerieID = dbo.SerieID,
                                         Nombre = dbo.Nombre,
                                         ImagenPortada = dbo.ImagenPortada,
                                         NombreProductora = dbo1.Nombre,
                                         GeneroPrimario = dbo2.Nombre,
                                         GeneroSecundario = dbo3 != null ? dbo3.Nombre : (string?)null

                                     }).AsNoTracking()
                                     .ToListAsync();
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Hubo un error obteniendo las series.";
                _logger.LogError(result.Message, ex.ToString());
            }
            return result;
        }


        public async override Task<OperationResult> GetById(int id)
        {
            OperationResult result = new OperationResult();

            try
            {
                result.Data = await (from dbo in _itlaTVContext.Series
                                     where dbo.SerieID == id

                                     select new Series()
                                     {
                                         SerieID = dbo.SerieID,
                                         Nombre = dbo.Nombre,
                                         ImagenPortada = dbo.ImagenPortada,
                                         EnlaceVideo = dbo.EnlaceVideo,
                                         ProductoraID = dbo.ProductoraID,
                                         GeneroPrimarioID = dbo.GeneroPrimarioID,
                                         GeneroSecundarioID = dbo != null ? dbo.GeneroSecundarioID : (int?)null
                                         
                                     }).AsNoTracking()
                                     .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Hubo un error obteniendo la serie.";
                _logger.LogError(result.Message, ex.ToString());
            }
            return result;
        }

        public async Task<OperationResult> GetSeriesByGenres(string seriesName, int generoId, int productoraId)
        {
            OperationResult result = new OperationResult();

            try
            {
                result.Data = await (from s in _itlaTVContext.Series
                                     join p in _itlaTVContext.Productoras on s.ProductoraID equals p.ProductoraID
                                     join g1 in _itlaTVContext.Generos on s.GeneroPrimarioID equals g1.GeneroID into generoPrimario
                                     from g1 in generoPrimario.DefaultIfEmpty()
                                     join g2 in _itlaTVContext.Generos on s.GeneroSecundarioID equals g2.GeneroID into generoSecundario
                                     from g2 in generoSecundario.DefaultIfEmpty()

                                     where (string.IsNullOrEmpty(seriesName) || s.Nombre.Contains(seriesName)) &&
                                           (generoId == 0 || s.GeneroPrimarioID == generoId || s.GeneroSecundarioID == generoId) && (productoraId == 0 || p.ProductoraID == productoraId)

                                     orderby s.SerieID descending

                                     select new SeriesViewModel()
                                     {
                                         SerieID = s.SerieID,
                                         Nombre = s.Nombre,
                                         ImagenPortada = s.ImagenPortada,
                                         NombreProductora = p.Nombre,
                                         GeneroPrimario = g1 != null ? g1.Nombre : null,
                                         GeneroSecundario = g2 != null ? g2.Nombre : null

                                     }).AsNoTracking()
                                     .ToListAsync();
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Hubo un error obteniendo las series.";
                _logger.LogError(result.Message, ex.ToString());
            }

            return result;
        }



    }
}
