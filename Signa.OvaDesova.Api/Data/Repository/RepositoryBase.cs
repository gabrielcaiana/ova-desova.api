using Dapper;
using Signa.Library;
using System.Data;
using System.Data.SqlClient;

namespace Signa.OvaDesova.Api.Data.Repository
{
    public abstract class RepositoryBase
    {
        public const string _n = "\n";
        public static int _usuarioId;
        public static int _empresaId;


        public static IDbConnection Connection
        {
            get
            {
                var conn = new SqlConnection(Global.ConnectionString);
                conn.Open();
                conn.Execute("Set Transaction Isolation Level Read UnCommitted");
                return conn;
            }
        }

        public RepositoryBase()
        {
            DefaultTypeMap.MatchNamesWithUnderscores = true;
        }
    }
}