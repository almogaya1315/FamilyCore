﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace FCore.Common.Utils
{
    public static class InputHelper
    {
        public static string GetFilePath(HttpPostedFileBase file)
        {
            if (file != null)
            {
                string pic = Path.GetFileName(file.FileName);
                return $"~/Images/Profiles/{pic}";
            }
            else return string.Empty;
        }

        public static void UploadProfileImage(HttpPostedFileBase file)
        {
            string pic = Path.GetFileName(file.FileName);
            string path = Path.Combine(HttpContext.Current.Server.MapPath("~/Images/Profiles/"), pic);
            file.SaveAs(path);
        }
    }
}