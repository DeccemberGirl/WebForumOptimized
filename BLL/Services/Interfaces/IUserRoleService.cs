using BLL.DTO;
using BLL.Infrastructure;
using System;
using System.Threading.Tasks;

namespace BLL.Services.Interfaces
{
    public interface IUserRoleService : IDisposable
    {
        Task<OperationDetails> ToggleAdmin(string id);

        
    }
}