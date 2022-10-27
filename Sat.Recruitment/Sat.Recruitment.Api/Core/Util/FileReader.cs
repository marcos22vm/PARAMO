using System;
using System.IO;

namespace Sat.Recruitment.Api.Core.Util
{
    public class FileReader
    {
        public static StreamReader ReadFile(string pathFile)
        {
            var path = Directory.GetCurrentDirectory() + pathFile;
            FileStream fileStream = new FileStream(path, FileMode.Open);
            StreamReader reader = new StreamReader(fileStream);
            return reader;
        }
    }
}
