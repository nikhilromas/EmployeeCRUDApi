using EmployeeCRUDApi.Models;

namespace EmployeeCRUDApi.Service
{
    public interface IEmployeeRepo
    {
        Task<List<Employee>> GetAll();
        Task<Employee> Getbycode(int code);
        Task<string> Create(Employee employee);
        Task<string> Update(Employee employee, int code);
        Task<string> Remove(int code);


    }
}
