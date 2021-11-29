
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Repository
{
   public class ClsCommon
    {
        GCTL_ERP_DB_MVC_06_27Entities db = new GCTL_ERP_DB_MVC_06_27Entities();

        public void FindMaxNo(ref string strMaxNo, string strFldName, string strTableName, int intLenWithPadding)
        {
            try
            {
                int Company = db.Database.SqlQuery<int>("Select isnull(MAX(convert(int," + strFldName + "))+1,0) as MaxNo from " + strTableName + "").FirstOrDefault<int>();
                if (Company != 0)
                {
                    strMaxNo = Company.ToString();
                    strMaxNo = strMaxNo.PadLeft(intLenWithPadding, '0');
                }
                else
                {
                    strMaxNo = "1";
                    strMaxNo = strMaxNo.PadLeft(intLenWithPadding, '0');
                }
            }
            catch (System.Exception ex)
            {
                string a = ex.Message.ToString();

            }
            finally
            {

            }
        }
        public void FindMaxNoAuto(ref string strMaxNo, string strFldName, string strTableName)
        {
            try
            {
                string Query = "Select isnull(MAX(convert(int," + strFldName + "))+1,0) as MaxNo from " + strTableName + "";
                int Company = db.Database.SqlQuery<int>(Query).FirstOrDefault<int>();
                if (Company != 0)
                {
                    strMaxNo = Company.ToString();

                }
                else
                {
                    strMaxNo = "1";

                }
            }
            catch (System.Exception ex)
            {
                string a = ex.Message.ToString();

            }
            finally
            {

            }
        }
        public void FindMaxGCTL(ref string strMaxNo, string strFldName, string strTableName, int intLenWithPadding, string WhereColumn, string WhereValue)
        {

            try
            {

                string QUery = "Select isnull(max(right(" + strFldName + ",6)),0)+1 as MaxNo from " + strTableName + " where left(right(" + WhereColumn + ",8),2)='" + WhereValue + "'";
                int Company = db.Database.SqlQuery<int>(QUery).FirstOrDefault<int>();

                if (Company != 0)
                {
                    strMaxNo = Company.ToString();
                    strMaxNo = strMaxNo.PadLeft(intLenWithPadding, '0');

                }
                else
                {
                    strMaxNo = "1";
                    strMaxNo = strMaxNo.PadLeft(intLenWithPadding, '0');
                }



            }
            catch (System.Exception ex)
            {
                string a = ex.Message.ToString();

            }
            finally
            {


            }
        }

        public Model_WeekendData GetCompanyWeekend()
        {
            var returnModel = new Model_WeekendData();
            using (var db = new GCTL_ERP_DB_MVC_06_27Entities())
            {
                var cmd = db.Database.Connection.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "dbo.GetWeekend";
                try
                {
                    db.Database.Connection.Open();
                    var reader = cmd.ExecuteReader();
                    var results = ((System.Data.Entity.Infrastructure.IObjectContextAdapter)db).ObjectContext.Translate<Model_WeekendData>(reader);
                    returnModel = (from s in results select s).FirstOrDefault();
                }
                catch (Exception ex)
                {
                    //ErrorSignal.FromCurrentContext().Raise(ex, HttpContext.Current);
                }
                finally
                {
                    db.Database.Connection.Close();
                }
            }
            return returnModel;
        }

        public Model_WeekendData GetLeaveDays()
        {
            var returnModel = new Model_WeekendData();
            using (var db = new GCTL_ERP_DB_MVC_06_27Entities())
            {
                var cmd = db.Database.Connection.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "dbo.GetWeekend";
                try
                {
                    db.Database.Connection.Open();
                    var reader = cmd.ExecuteReader();
                    var results = ((System.Data.Entity.Infrastructure.IObjectContextAdapter)db).ObjectContext.Translate<Model_WeekendData>(reader);
                    returnModel = (from s in results select s).FirstOrDefault();
                }
                catch (Exception ex)
                {
                    //ErrorSignal.FromCurrentContext().Raise(ex, HttpContext.Current);
                }
                finally
                {
                    db.Database.Connection.Close();
                }
            }
            return returnModel;
        }

        public Model_WeekendData GetEmployeeWeekend(string EmployeeID)
        {
            var returnModel = new Model_WeekendData();
            using (var db = new GCTL_ERP_DB_MVC_06_27Entities())
            {
                var cmd = db.Database.Connection.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "dbo.GetEmployeeWeekend";

                var sParam = cmd.CreateParameter();
                sParam.DbType = DbType.String;
                sParam.ParameterName = "@EmployeeID";
                sParam.Value = EmployeeID;
                sParam.IsNullable = false;
                cmd.Parameters.Add(sParam);
                try
                {
                    db.Database.Connection.Open();
                    var reader = cmd.ExecuteReader();
                    var results = ((System.Data.Entity.Infrastructure.IObjectContextAdapter)db).ObjectContext.Translate<Model_WeekendData>(reader);
                    returnModel = (from s in results select s).FirstOrDefault();
                }
                catch (Exception ex)
                {
                    //ErrorSignal.FromCurrentContext().Raise(ex, HttpContext.Current);
                }
                finally
                {
                    db.Database.Connection.Close();
                }
            }
            return returnModel;
        }
    }

}
