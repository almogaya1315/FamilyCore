using System;
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
        public static string GetFilePath(HttpPostedFileBase file, int libId = 0)
        {
            if (file != null)
            {
                string fileName = Path.GetFileName(file.FileName);
                if (file.ContentType.Contains("image")) return $"~/Images/Profiles/{fileName}";
                else if (file.ContentType.Contains("video"))
                {
                    if (libId == 0) throw new InvalidOperationException("The server did not pass the library id of the uploaded video file correctly.");
                    return $"~/Videos/libId#{libId}/{fileName}";
                }
            }
            return string.Empty;
        }

        public static void UploadProfileImage(HttpPostedFileBase file)
        {
            string pic = Path.GetFileName(file.FileName);
            string path = Path.Combine(HttpContext.Current.Server.MapPath("~/Images/Profiles/"), pic);
            file.SaveAs(path);
        }

        public static void UploadVideo(HttpPostedFileBase file, int libId)
        {
            string video = Path.GetFileName(file.FileName);
            string path = Path.Combine(HttpContext.Current.Server.MapPath($"~/Videos/libId#{libId}/"), video);
            string folderPath = path.Replace("\\" + file.FileName, string.Empty);
            Directory.CreateDirectory(folderPath);
            file.SaveAs(path);
        }
    }
}
