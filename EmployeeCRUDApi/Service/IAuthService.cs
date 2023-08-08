using EmployeeCRUDApi.Models;

namespace EmployeeCRUDApi.Service
{
    public interface IAuthService
    {

        Task<LoginRequest> GetByUserNameAndPassword(string userName, string password);

    }
}
