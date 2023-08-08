using Dapper;
using EmployeeCRUDApi.database;
using EmployeeCRUDApi.Models;


namespace EmployeeCRUDApi.Service
{
    public class AuthService : IAuthService
    {
        private readonly string _connectionString;

        private readonly DapperDBContext context;

        public AuthService (DapperDBContext context)
        {
           
            this.context = context;
        }

        public async Task<LoginRequest> GetByUserNameAndPassword(string userName, string password)
        {
            string query = "SELECT * FROM Users WHERE UserName = @UserName AND sPassword = @Password";

            using (var connection = this.context.Createconnection())
            {
                var login = await connection.QueryFirstOrDefaultAsync<LoginRequest>(query, new { UserName = userName, Password = password });
                return login;
            }
        }




   

    }
}
