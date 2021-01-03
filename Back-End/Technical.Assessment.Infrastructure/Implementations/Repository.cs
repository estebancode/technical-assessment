using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Technical.Assessment.Domain.Entities;
using Technical.Assessment.Domain.Interfaces;
using Technical.Assessment.Infrastructure.Context;

namespace Technical.Assessment.Infrastructure.Implementations
{
    /// <summary>
    /// repository's class
    /// </summary>
    /// <typeparam name="E"></typeparam>
    public class Repository<E> : IRepository<E> where E : DomainEntity
    {
        /// <summary>
        /// context's instance
        /// </summary>
        protected readonly TechnicalAssessmentContext context;

        /// <summary>
        /// Entity's instance
        /// </summary>
        protected readonly DbSet<E> entities;

        /// <summary>
        /// context's instance
        /// </summary>
        /// <param name="context"></param>
        public Repository(TechnicalAssessmentContext context)
        {
            this.context = context;
            entities = this.context.Set<E>();
        }

        /// <summary>
        /// Delete entity
        /// </summary>
        /// <param name="entity"></param>
        public void Delete(E entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            entities.Remove(entity);
        }

        /// <summary>
        ///  get async entity
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<E> GetAsync(object id)
        {
            return await this.entities.FindAsync(id).ConfigureAwait(false);
        }

        /// <summary>
        /// get all entitites
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="orderBy"></param>
        /// <param name="includeProperties"></param>
        /// <param name="isTracking"></param>
        /// <returns></returns>
        public async virtual Task<IEnumerable<E>> GetAllAsync(Expression<Func<E, bool>> filter,
            Func<IQueryable<E>, IOrderedQueryable<E>> orderBy,
            string includeProperties, bool isTracking)
        {
            IQueryable<E> query = entities;

            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties.Split
                (new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }

            if (orderBy != null)
            {
                return await orderBy(query).ToListAsync().ConfigureAwait(false);
            }

            return (!isTracking) ? await query.AsNoTracking().ToListAsync().ConfigureAwait(false)
                : await query.ToListAsync().ConfigureAwait(false);

        }

        /// <summary>
        /// insert entity
        /// </summary>
        /// <param name="entity"></param>
        public void Insert(E entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            entities.Add(entity);
        }

        /// <summary>
        /// save changes async
        /// </summary>
        /// <returns></returns>
        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync().ConfigureAwait(false);

        }

        /// <summary>
        /// update entity
        /// </summary>
        /// <param name="entity"></param>
        public void Update(E entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            entities.Update(entity);
        }

        public async Task UpdateAsync(E entity)
        {
            if (entity != null)
            {
                await Task.Run(() => entities.Update(entity)).ConfigureAwait(false);

            }
        }

        /// <summary>
        /// Get by async 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public async Task<E> GetByAsync(Expression<Func<E, bool>> expression)
        {
            return await this.entities.SingleOrDefaultAsync<E>(expression).ConfigureAwait(false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entitys"></param>
        /// <returns></returns>
        public void AddRange(IEnumerable<E> entitys)
        {
            entities.AddRange(entitys);
        }

        /// <summary>
        /// save changes 
        /// </summary>
        /// <returns></returns>
        public void SaveChanges()
        {
            context.SaveChanges();
        }

        public void UpdateRange(IEnumerable<E> entitys)
        {
            entities.UpdateRange(entitys);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


        protected virtual void Dispose(bool disposing)
        {
            this.context.Dispose();
        }
    }
}
