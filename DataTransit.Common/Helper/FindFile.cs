using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransit.Common.Helper
{
    public static class FindFile
    {
        private static FileInfo SendFile(string path, string extension)
        {
            var directory = new DirectoryInfo(path);
            var file = directory.GetFiles(extension).FirstOrDefault();
            return file;
        }
        public static FileInfo GetFile(string path, string extension)
        {
            var fileName = Path.GetFileName(path);
            if (string.IsNullOrEmpty(fileName))
            {
                return SendFile(path, extension);
            }
            else
            {
                var directoryName = Path.GetDirectoryName(path);
                return SendFile(directoryName, extension);
            }
        }

        public static string CorrectFilePath(string path)
        {
            var ext = Path.GetExtension(path);
            if (string.IsNullOrEmpty(ext))
            {
                var file = SendFile(path, "*.csv");
                var fullPath = file.FullName;
                return fullPath;
            }
            return path;
        }
    }
}
