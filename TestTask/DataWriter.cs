using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTask
{
    static class DataWriter
    {
        public static void Write(string mainPath, List<string> listPath)
        {
            try
            {
                // Delete the file if it exists.
                if (File.Exists(mainPath))
                {
                    File.Delete(mainPath);
                }

                using (FileStream fs = File.Create(mainPath))
                {
                    foreach (string path in listPath)
                    {
                        byte[] info = new UTF8Encoding(true).GetBytes(path + Environment.NewLine);
                        // Add some information to the file.
                        fs.Write(info, 0, info.Length);
                    }
                }
                Console.WriteLine("File with result is created: {0}", mainPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Cant write info in file with exeption:");
                Console.WriteLine(ex.Message);
            }
        }
    }
}
