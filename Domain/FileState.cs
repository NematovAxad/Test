using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Domain.States;
using System.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace Domain
{
    public static class FileState
    {
        public static string AddFile(string apiName, string dirName, IFormFile newFile)
        {
            var path = Directory.GetCurrentDirectory();
            var name = Guid.NewGuid().ToString();
            //dirName = "reestrfiles/" + dirName;
            path = Path.Combine(path, "wwwroot", apiName, "documents", dirName);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            byte[] bytes = null;
            using (var binaryReader = new BinaryReader(newFile.OpenReadStream()))
            {
                bytes = binaryReader.ReadBytes((int)newFile.Length);
            }
            var fileName = CombinateFileName(newFile.FileName);

            path = Path.Combine(path, fileName);
            if (bytes.Length == 0)
            {

            }
            Console.WriteLine(path);
            File.WriteAllBytes(path, bytes);
            return FileUrl(apiName, dirName, fileName);

        }
        public static string AddFile(string apiName, string dirName, string newFile)
        {
            var path = Directory.GetCurrentDirectory();
            var name = $"{Guid.NewGuid()}.jpg";
            //dirName = "reestrfiles/" + dirName;
            path = Path.Combine(path, "wwwroot", apiName, "documents", dirName);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
             
            byte[] bytes = Convert.FromBase64String(newFile);

            path = Path.Combine(path, name);
            if (bytes.Length == 0)
            {

            }

            

            Console.WriteLine(path);
            File.WriteAllBytes(path, bytes);
            return FileUrl(apiName, dirName, name);

        }
        public static string FileUrl(string apiName, string dirName, string fileName)
        {
            return AuthOptions.FilePath + "/" + apiName + "/" + "documents/" + dirName + "/" + fileName;
        }

        public static string CombinateFileName(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                return CheckingState.RandomString(6);
            }
            var fileTip = fileName.Split('.').Last();
            var guid = CheckingState.RandomString(8);
            return guid + "." + fileTip;
        }
    }
}
