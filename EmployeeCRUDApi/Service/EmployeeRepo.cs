using Dapper;
using EmployeeCRUDApi.database;
using EmployeeCRUDApi.Models;
using System.Data;

namespace EmployeeCRUDApi.Service
{
    public class EmployeeRepo: IEmployeeRepo
    {
        private readonly DapperDBContext context;
        public EmployeeRepo(DapperDBContext context) 
        {
        
            this.context = context;
        
        }

        public async Task<List<Employee>> GetAll()
        {
            string query = "Select * From Employees";
            using (var connectin = this.context.Createconnection())
            {
                var emplist = await connectin.QueryAsync<Employee>(query);
                return emplist.ToList();
            }
        }

        public async Task<string> Create(Employee employee)
        {
            string response = string.Empty;
            string query = "Insert into Employees(name,email,phone,designation) values (@name,@email,@phone,@designation)";
            var parameters = new DynamicParameters();
            parameters.Add("name", employee.Name, DbType.String);
            parameters.Add("email", employee.Email, DbType.String);
            parameters.Add("phone", employee.Phone, DbType.String);
            parameters.Add("designation", employee.Designation, DbType.String);
            using (var connectin = this.context.Createconnection())
            {
                await connectin.ExecuteAsync(query, parameters);
                response = "pass";
            }
            return response;
        }

        public async Task<Employee> Getbycode(int code)
        {
            string query = "Select * From Employees where code=@code";
            using (var connectin = this.context.Createconnection())
            {
                var emplist = await connectin.QueryFirstOrDefaultAsync<Employee>(query, new { code });
                return emplist;
            }
        }

        public async Task<string> Update(Employee employee, int code)
        {
            string response = string.Empty;
            string query = "update Employees set name=@name,email=@email,phone=@phone,designation=@designation where code=@code";
            var parameters = new DynamicParameters();
            parameters.Add("code", code, DbType.Int32);
            parameters.Add("name", employee.Name, DbType.String);
            parameters.Add("email", employee.Email, DbType.String);
            parameters.Add("phone", employee.Phone, DbType.String);
            parameters.Add("designation", employee.Designation, DbType.String);
            using (var connectin = this.context.Createconnection())
            {
                await connectin.ExecuteAsync(query, parameters);
                response = "pass";
            }
            return response;
        }

        public async Task<string> Remove(int code)
        {
            string response = string.Empty;
            string query = "Delete From Employees where code=@code";
            using (var connectin = this.context.Createconnection())
            {
                await connectin.ExecuteAsync(query, new { code });
                response = "pass";
            }
            return response;
        }

    }
}


