using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.src.Repositories
{
    public class DatabaseRepository
    {
        protected readonly string connectionString =
            "User Id=rm552568;Password=171204;Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=oracle.fiap.com.br)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=ORCL)))";
    }
}