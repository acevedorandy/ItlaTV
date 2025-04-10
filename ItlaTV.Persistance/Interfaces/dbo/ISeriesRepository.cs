

using ItlaTV.Domain.Entities.dbo;
using ItlaTV.Domain.Repositories;
using ItlaTV.Domain.Result;

namespace ItlaTV.Persistance.Interfaces.dbo
{
    public interface ISeriesRepository : IBaseRepository<Series>
    {
        Task<OperationResult> GetSeriesByGenres(string seriesName, int genresId, int productoraId);
    }
}
