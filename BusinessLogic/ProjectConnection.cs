using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FJS
{
    public class ProjectConnection
    {
        public static SqlConnection conn = null;
        public void connection_today()
        {
            //conn = new SqlConnection("Data Source=VMD51868;Initial Catalog=GCTL_ATTENDENCE_DB;User ID=sa;Password=GCTL#123");
            //conn = new SqlConnection("Data Source=DESKTOP-IKKC33C;Initial Catalog=Att_DB;User ID=sa;Password=GCTL#123");
            conn = new SqlConnection("Data Source=ESTIAK45461;Initial Catalog=Att_DB;User ID=sa;Password=Walton@2021");
        }

        public ProjectConnection()
        {
            //
            // TODO: Add constructor logic here
            //
        }
    }
}
