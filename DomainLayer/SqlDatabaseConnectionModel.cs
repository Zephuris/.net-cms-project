using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer
{
    public class SqlDatabaseConnectionModel : DatabaseConnectionModel
    {
        public string ConnectionString { get { return @$"Server= {servername}; Database={databaseName}; Trusted_Connection=True"; } }
    }
}
