using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace DL
{
    public class Connection
    {
        //Estableciendo
        public static string GetConnection()
        {
            string connection = ConfigurationManager.ConnectionStrings["UMarquezProgramacionNCapas"].ToString();
            return connection;
        }
    }
}
