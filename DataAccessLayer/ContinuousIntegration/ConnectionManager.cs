using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContinuousIntegration
{
    public class ConnectionManager
    {
        public string ConnectionString
        {
            get
            {
                return @"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=Debug;Integrated Security=True";
            }
        }
    }
}
