using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTask
{
    public class PathParser
    {
        private List<string> resultList = new List<string>();

        public List<string> Result
        {
            get { return resultList; }
        }

        public async Task GetCCPPathAsync(string mainPath)
        {
            try
            {
                try
                {
                    string[] fileinfos = new DirectoryInfo(mainPath).GetFiles("*.cpp").Select(f => f.Name).ToArray();
                    if (fileinfos != null)
                    {
                        foreach (string f in fileinfos)
                        {
                            string dir = Cut(mainPath);
                            dir = Path.Combine(dir, f);
                            resultList.Add(dir);
                        }
                    }

                }
                catch (FileNotFoundException e)
                {
                    Console.WriteLine(e.Message);
                }

                string[] directories = Directory.GetDirectories(mainPath);
                foreach (string directory in directories)
                {
                    await GetCCPPathAsync(directory);
                }
            }
            catch (UnauthorizedAccessException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (DirectoryNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public async Task GetAllPathAsync(string mainPath, bool reverse1, bool reverse2)
        {
            try
            {
                string[] dirinfos = Directory.GetDirectories(mainPath);
                foreach (string directory in Directory.GetDirectories(mainPath))
                {
                    if (!reverse1 && !reverse2)
                    {
                        string dir = Cut(directory);
                        resultList.Add(dir);
                    }
                    else if (reverse1 || reverse2)
                    {
                        string dir = Cut(directory);
                        string reversedDir = (reverse1) ? Reverse1(dir) : Reverse2(dir);
                        resultList.Add(reversedDir);
                    }
                    await GetAllPathAsync(directory, reverse1, reverse2);
                }
            }
            catch (UnauthorizedAccessException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (DirectoryNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private string Reverse1(string defaultPath)
        {
            char[] charsPath = defaultPath.ToArray();
            List<string> folders = new List<string>();
            string folder = null;

            for(int c = 0; c < charsPath.Length; c++)
            {
                if (charsPath[c] != '\\')
                {
                    folder += charsPath[c];
                }
                if (c == charsPath.Length - 1)
                {
                    folders.Add(folder);
                }
                if (c != 0 && charsPath[c] == '\\') 
                {
                    folders.Add(folder);
                    folder = "";
                }
            }
            string reversePath = "";
            for (int i = folders.Count - 1; i > -1; i--)
            {
                reversePath += folders[i];
                if (i > 0) reversePath += "\\";
            }
            return reversePath;
        }

        private string Reverse2(string defaultPath)
        {
            List<char> charsPath = defaultPath.ToList();
            string reversePath = "";

            for (int ch = charsPath.Count -1; ch >= 0; ch--)
            {
                reversePath += charsPath[ch];
            }
            return reversePath;
        }

        private string Cut(string directory)
        {
           return directory = directory.Substring(Program.path.Length);
        }
    }
}
