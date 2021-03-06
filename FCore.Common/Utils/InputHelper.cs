﻿using FCore.Common.Models.Videos;
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
                if (file.ContentType.Contains("image")) return $"{ConstGenerator.ImagePath}{fileName}";
                else if (file.ContentType.Contains("video"))
                {
                    if (libId == 0) throw new InvalidOperationException("The server did not pass the library id of the uploaded video file correctly.");
                    return $"{ConstGenerator.VideoPath}{libId}/{fileName}";
                }
            }
            return string.Empty;
        }

        public static void UploadProfileImage(HttpPostedFileBase file)
        {
            string pic = Path.GetFileName(file.FileName);
            string path = Path.Combine(HttpContext.Current.Server.MapPath($"{ConstGenerator.ImagePath}"), pic);
            file.SaveAs(path);
        }

        public static void UploadVideo(HttpPostedFileBase file, int libId)
        {
            string video = Path.GetFileName(file.FileName);
            string path = Path.Combine(HttpContext.Current.Server.MapPath($"{ConstGenerator.VideoPath}{libId}/"), video);
            string folderPath = path.Replace("\\" + file.FileName, string.Empty);
            Directory.CreateDirectory(folderPath);
            file.SaveAs(path);
        }

        public static void DeleteVideo(VideoModel video, int libId)
        {
            string path = Path.Combine(HttpContext.Current.Server.MapPath($"{video.Path}"));
            File.Delete(path);
            string folderPath = Path.Combine(HttpContext.Current.Server.MapPath($"{ConstGenerator.VideoPath}{libId}"));
            if (Directory.GetFiles(folderPath).Count() == 0)
                Directory.Delete(folderPath);
        }
    }
}
