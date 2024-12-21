using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Common.Application.Functions
{
    public static class FileReader
    {
        public static string ReadAsText(string path)
        {
            using (StreamReader streamReader = new StreamReader(path))
            {
                string readedString = streamReader.ReadToEnd();
                return readedString;
            }
        }
    }
}
