using DAL.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Repositories.Interfaces
{
    /// <summary>
    /// Generic repository interface  
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        /// <summary>
        /// Gets all objects of the collection from the repository
        /// </summary>
        /// <returns>Collection of entities</returns>
        IQueryable<TEntity> FindAll();

        /// <summary>
        /// Gets the concrete object from the repository by its id
        /// </summary>
        /// <param name="id">Entity Id</param>
        /// <returns>Collection of entities</returns>
        Task<TEntity> GetByIdAsync(int id);

        /// <summary>
        /// Adds object to the repository 
        /// </summary>
        /// <param name="entity">Entity object</param>
        /// <returns>Entity Id</returns>
        Task<int> AddAsync(TEntity entity);

        /// <summary>
        /// Updates the object info in the repository
        /// </summary>
        /// <param name="entity">Entity object</param>
        Task Update(TEntity entity);

        /// <summary>
        /// Deletes the object from the repository
        /// </summary>
        /// <param name="entity">Entity object</param>
        void Delete(TEntity entity);

        /// <summary>
        /// Deletes the object from the repository by its id
        /// </summary>
        /// <param name="id">Entity Id</param>
        Task DeleteByIdAsync(int id);
    }
}
