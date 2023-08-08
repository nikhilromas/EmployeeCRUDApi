using Microsoft.Data.SqlClient;
using System.Data;

namespace EmployeeCRUDApi.database
{
    public class DapperDBContext
    {
        private readonly IConfiguration _configuration;
        private readonly string connectionstring;

        public DapperDBContext(IConfiguration configuration)
        {
            this._configuration = configuration;
            this.connectionstring = this._configuration.GetConnectionString("DefaultConnection");
        }

        public IDbConnection Createconnection() => new SqlConnection(connectionstring);
    }
}
