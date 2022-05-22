using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR
{
    class ConnectionTestDb
    {
        public string GetConnection()
        {
            //String con = "Data Source=LAB1-WS03;Initial Catalog=TEST;User ID=sa;Password=d2h2mqqa";
            String con = "Data Source=SMRU-DBT-MST;Initial Catalog=HR-DA;User ID=sa;Password=smrudbt@2019";
            return con;
        }
    }
}
