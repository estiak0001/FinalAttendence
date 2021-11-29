using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Repository
{
    public class Crud_Core_AccessCode2
    {
        public List<Model_Core_AccessCode2> GetAllInfo()
        {
           
            using (var context = new GCTL_ERP_DB_MVC_06_27Entities())
            {

                var result = (from mo in context.Core_AccessCode2
                              select new
                              {
                                  AccessCodeID = mo.AccessCodeID,
                                  AccessCodeName = mo.AccessCodeName
                                
                              }).Distinct().AsEnumerable().Select(a => new Model_Core_AccessCode2()
                              {
                                  AccessCodeID = a.AccessCodeID.ToString(),
                                  AccessCodeName = a.AccessCodeName
                              }).ToList();

                return result;
            }
        }

        
        public Model_Core_AccessCode2 GetInfo(string AccessCodeID)
        {
            
            var db = new GCTL_ERP_DB_MVC_06_27Entities();
            var result = (from psi in db.Core_AccessCode2
                          .Where(psi => psi.AccessCodeID ==AccessCodeID).DefaultIfEmpty().AsEnumerable()
                          select new
                          {
                              AccessCodeID = psi.AccessCodeID,
                              AccessCodeName = psi.AccessCodeName,
                             

                          }).AsEnumerable().Select(a => new Model_Core_AccessCode2()
                          {

                              AccessCodeID = a.AccessCodeID,
                              AccessCodeName = a.AccessCodeName,
                              
                          }).FirstOrDefault();
            return result;
        }

      
        public bool DeleteInfo(string AccessCodeID)
        {
            var context = new GCTL_ERP_DB_MVC_06_27Entities();
            var result = context.Core_AccessCode2.Where(x => x.AccessCodeID == AccessCodeID).ToList();
            if (result != null)
            {
                context.Core_AccessCode2.RemoveRange(result);
                context.SaveChanges();
            }
            return false;
        }

        public List<Model_Core_AccessCode2> GetAccessCodeGrid(string AccessCodeID)
        {
            var returnModel = new List<Model_Core_AccessCode2>();
            using (var db = new GCTL_ERP_DB_MVC_06_27Entities())
            {
                var cmd = db.Database.Connection.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "dbo.Prc_GetAccessCode";
                var sParam = cmd.CreateParameter();
                sParam.DbType = DbType.String;
                sParam.ParameterName = "@AccessCodeID";
                sParam.Value = AccessCodeID;
                sParam.IsNullable = false;
                cmd.Parameters.Add(sParam);
                try
                {
                    

                    db.Database.Connection.Open();
                    var reader = cmd.ExecuteReader();
                    var results = ((IObjectContextAdapter)db).ObjectContext.Translate<Model_Core_AccessCode2>(reader);
                    returnModel = (from s in results select s).ToList();
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
        public List<Model_Manutab> GetAllMenutab()
        {
            using (var context = new GCTL_ERP_DB_MVC_06_27Entities())
            {
                var result = (from mo in context.Core_MenuTab2
                              select new
                              {
                                  title = mo.title,
                                  ParentID = mo.ParentID,
                                  MenuId = mo.MenuId,
                                  OrderBy = mo.OrderBy,
                                  ControllerName = mo.ControllerName,
                                  ViewName = mo.ViewName,
                                  Icon = mo.Icon
                              }).AsEnumerable().Select(a => new Model_Manutab()
                              {
                                  title = a.title.ToString(),
                                  ParentID = a.ParentID,
                                  MenuId = a.MenuId,
                                  OrderBy = Convert.ToInt32(a.OrderBy),
                                  ControllerName = a.ControllerName,
                                  ViewName = a.ViewName,
                                  Icon = a.Icon,
                                  chkAdd = "",
                                  chkEdit="",
                                  chkDelete="",
                                  chkPrint="",
                                  TitleCheck=""
                              }).OrderBy(p=>p.OrderBy).ToList();

                return result;
            }
        }

    }
}
