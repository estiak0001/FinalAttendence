using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BusinessLogic.Repository
{
    public class MaxFileSizeAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)

        {

            int maxContent = 1024 * 1024; //1 MB

            string[] sAllowedExt = new string[] { ".jpg", ".gif", ".png", ".JPG", ".PNG", ".PNG" };

            var file = value as HttpPostedFileBase;



            if (file != null)
            {
                if (!sAllowedExt.Contains(file.FileName.Substring(file.FileName.LastIndexOf('.'))))

                {
                    ErrorMessage = "You can upload only jpg,png,gif extension file";
                    return false;
                }
                else if (file.ContentLength > maxContent)
                {
                    ErrorMessage = "Your Photo is too large, maximum allowed size is 1 MB";
                    return false;
                }

                else

                    return true;
            }
            return true;
        }
    }
}