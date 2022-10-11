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

        public async Task<IEnumerable<TEntity>> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return (IEnumerable<TEntity>)await EntitySet.FindAsync(predicate);
        }

        public async Task<TEntity> Get(int id)
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

    //public class RCopy
    //{
    //    public void Add(TEntity entity)
    //    {
    //        Context.Set<TEntity>().Add(entity);
    //    }

    //    public void AddRange(IEnumerable<TEntity> entities)
    //    {
    //        Context.Set<TEntity>().AddRange(entities);
    //    }

    //    public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
    //    {
    //        return Context.Set<TEntity>().Where(predicate);
    //    }

    //    public TEntity Get(int id)
    //    {
    //        return Context.Set<TEntity>().Find(id);
    //    }

    //    public IEnumerable<TEntity> GetAll()
    //    {
    //        return Context.Set<TEntity>().ToList();
    //    }

    //    public void Remove(TEntity entity)
    //    {
    //        Context.Set<TEntity>().Remove(entity);
    //    }

    //    public void RemoveRange(IEnumerable<TEntity> entities)
    //    {
    //        Context.Set<TEntity>().RemoveRange(entities);
    //    }
    //}
}
