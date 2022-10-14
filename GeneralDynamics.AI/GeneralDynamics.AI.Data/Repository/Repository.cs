using GeneralDynamics.AI.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GeneralDynamics.AI.Data.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {

        protected readonly GeneralDynamicsAIContext _context;

        // public abstract Task<IEnumerable<TEntity>> Get(Expression<Func<TEntity, bool>> filter = null, Func<System.Linq.IQueryable<TEntity>, System.Linq.IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = null);

        public Repository(GeneralDynamicsAIContext context)
        {
            _context = context;
        }

        protected DbSet<TEntity> EntitySet
        {
            get
            {
                return _context.Set<TEntity>();
            }
        }

        public async Task Add(TEntity entity)
        {
            await EntitySet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task AddRange(IEnumerable<TEntity> entities)
        {
            await EntitySet.AddRangeAsync(entities);
            await _context.SaveChangesAsync();
        }

        public async Task Update(TEntity entity)
        {
            EntitySet.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TEntity>> Find(Expression<Func<TEntity, bool>> predicate = null)
        {
            var userList =  (IEnumerable<TEntity>)await EntitySet.ToListAsync();

            return userList.Where(predicate.Compile());
        }

        public async Task<TEntity> FindOne(Expression<Func<TEntity, bool>> predicate = null)
        {
            return (TEntity)await EntitySet.FirstOrDefaultAsync(predicate);
        }

        public async Task<TEntity> GetById(int id)
        {
            return await EntitySet.FindAsync(id);
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await EntitySet.ToListAsync();
        }

        public async Task Remove(TEntity entity)
        {
            EntitySet.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveRange(IEnumerable<TEntity> entities)
        {
            EntitySet.RemoveRange(entities);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TEntity>> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = null)
        {
            IQueryable<TEntity> query = await GetQuery(filter, orderBy, includeProperties);
            return query;
        }

        protected virtual async Task<IQueryable<TEntity>> GetQuery(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
        {
            IQueryable<TEntity> query = EntitySet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (!String.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty.Trim());
                }
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }
            return query;
        }

        #region IDisposable Support
        private bool disposedValue = false; // Para detectar llamadas redundantes

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: elimine el estado administrado (objetos administrados).
                }

                // TODO: libere los recursos no administrados (objetos no administrados) y reemplace el siguiente finalizador.
                // TODO: configure los campos grandes en nulos.

                disposedValue = true;
            }
        }

        // TODO: reemplace un finalizador solo si el anterior Dispose(bool disposing) tiene código para liberar los recursos no administrados.
        // ~CoberturaService()
        // {
        //   // No cambie este código. Coloque el código de limpieza en el anterior Dispose(colocación de bool).
        //   Dispose(false);
        // }

        // Este código se agrega para implementar correctamente el patrón descartable.
        public void Dispose()
        {
            // No cambie este código. Coloque el código de limpieza en el anterior Dispose(colocación de bool).
            Dispose(true);
            // TODO: quite la marca de comentario de la siguiente línea si el finalizador se ha reemplazado antes.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }

}
