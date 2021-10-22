using DAL.Entities;
using System;
using System.Threading.Tasks;

namespace DAL.Repositories.Interfaces
{
    /// <summary>
    /// Forum profiles manager interface
    /// </summary>
    public interface IForumProfileManager : IDisposable
    {
        /// <summary>
        /// Creates forum profile object
        /// </summary>
        /// <param name="item">ForumProfile entity</param>
        void Create(ForumProfile item);

        /// <summary>
        /// Deletes forum profile object
        /// </summary>
        /// <param name="item">ForumProfile</param>
        Task Delete(ForumProfile item);
    }
}
