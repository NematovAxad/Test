using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Domain.States;
using System.Linq;

namespace ApiConfigs
{
    public static class FileState
    {
        public static string AddFile(string dirName, IFormFile newFile)
        {
            var path = Directory.GetCurrentDirectory();
            var name = Guid.NewGuid().ToString();
            //dirName = "reestrfiles/" + dirName;
            path = Path.Combine(path, "wwwroot", "pictures", dirName);
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
            return FileUrl(dirName, fileName);

        }

        public static string FileUrl(string dirName, string fileName)
        {
            return AuthOptions.FilePath + "/" + "pictures/" + dirName + "/" + fileName;
        }

        public static string CombinateFileName(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                return CheckingState.RandomString(6);
            }
            var fileTip = fileName.Split('.').Last();
            var guid = CheckingState.RandomString(6);
            return fileName.Substring(0, fileName.Length - fileTip.Length - 1) + guid + "." + fileTip;
        }
    }
}
