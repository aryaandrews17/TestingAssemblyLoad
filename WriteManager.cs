using System;
using System.IO;

namespace TestingAssemblyLoad
{
    public class WriteManager
    {
        public void WriteDependentServicesToFile(string NameOfService, string filePath)
        {
            string path = filePath + "\\Dependencies";
            Directory.CreateDirectory(path);
            string file = Path.Combine(path, "DependentEntities.txt");
            File.AppendAllText(file, string.Format(NameOfService + Environment.NewLine));
        }
        public void WriteIndependentServicesToFile(string NameOfService, string filePath)
        {
            string path = filePath + "\\Dependencies";

            System.IO.Directory.CreateDirectory(path);
            string file = Path.Combine(path, "IndependentEntities.txt");
            File.AppendAllText(file, string.Format(NameOfService + Environment.NewLine));

        }
    }
}
