

using ItlaTV.Domain.Repositories;
using ItlaTV.Domain.Result;
using ItlaTV.Persistance.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ItlaTV.Persistance.Base
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    { 
        private readonly ItlaTVContext itlatv_context;
        private DbSet<TEntity> entities;

        public BaseRepository(ItlaTVContext itlaTVContext)
        {
                itlatv_context = itlaTVContext;
            this.entities = itlatv_context.Set<TEntity>();
        }

        public virtual async Task<bool> Exists(Expression<Func<TEntity, bool>> filter)
        {
            return await this.entities.AnyAsync(filter);
        }

        public virtual async Task<OperationResult> GetAll()
        {
            OperationResult result = new OperationResult();

            try
            {
                var datos = await this.entities.ToListAsync();
                result.Data = datos;
            }
            catch (Exception)
            {
                result.Success = false;
                result.Message = "Ha ocurrido un error guardando los datos.";
            }
            return result;
        }

        public virtual async Task<OperationResult> GetById(int id)
        {
            OperationResult result = new OperationResult();

            try
            {
                var entity = await this.entities.FindAsync(id);
                result.Data = entity;
            }
            catch (Exception)
            {
                result.Success = false;
                result.Message = "Ha ocurrido un error obteniendo la entidad.";
            }
            return result;
        }

        public virtual async Task<OperationResult> Remove(TEntity entity)
        {
            OperationResult result = new OperationResult();

            try
            {
                entities.Remove(entity);
                await itlatv_context.SaveChangesAsync();
            }
            catch (Exception)
            {
                result.Success = false;
                result.Message = "Ha ocurrido un error borrando la entidad.";
            }
            return result;
        }

        public virtual async Task<OperationResult> Save(TEntity entity)
        {
            OperationResult result = new OperationResult();

            try
            {
                entities.Add(entity);
                await itlatv_context.SaveChangesAsync();
            }
            catch (Exception)
            {
                result.Success = false;
                result.Message = "Ha ocurrido un error al guardar la entidad.";
            }
            return result;
        }

        public virtual async Task<OperationResult> Update(TEntity entity)
        {
            OperationResult result = new OperationResult();

            try
            {
                entities.Update(entity);
                await itlatv_context.SaveChangesAsync();
            }
            catch (Exception)
            {
                result.Success = false;
                result.Message = "Ha ocurrido un error al actualizar la entidad.";
            }
            return result;
        }
    }
}
