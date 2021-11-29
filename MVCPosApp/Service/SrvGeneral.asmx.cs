using FJS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace MVCPosApp.Service
{
    /// <summary>
    /// Summary description for SrvGeneral
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class SrvGeneral : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }
        [WebMethod]
        public DataTable GetData(DataTable dt, string Query)
        {
            ProjectConnection con = new ProjectConnection();
            con.connection_today();
            ProjectConnection.conn.Close();
            if (ProjectConnection.conn.State == ConnectionState.Closed)
            {
                ProjectConnection.conn.Open();
                try
                {
                    SqlCommand cmd = new SqlCommand(Query, ProjectConnection.conn);
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    sda.Fill(dt);
                }
                catch (Exception ex)
                {

                    string exception = ex.Message;
                }
                finally
                {
                    ProjectConnection.conn.Close();
                    ProjectConnection.conn.Dispose();
                }
            }
            return dt;

        }
    }
}
