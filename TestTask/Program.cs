using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTask
{
    public class Program
    {
        enum UserControl { all = 1, cpp, r1, r2, exit }
        public static string path;
        static string command;

        static void Main(string[] args)
        {
            UserControl usercontrol = new UserControl();
            bool isExit = false;
            string fileName = "results.txt";

            while (!isExit)
            {
                List<string> resultList = new List<string>();
                string info = "";
                PathParser pp = new PathParser();
                Instruction();
                string filePath = path;
                string fullName = Path.Combine(path, fileName);
                Enum.TryParse(command, true, out usercontrol);

                switch (usercontrol)
                {
                    case UserControl.all:
                        info = "You choose all directories";
                        Console.WriteLine(info + "\n");
                        pp.GetAllPathAsync(path, false, false).GetAwaiter().GetResult();
                        resultList = pp.Result;
                        Console.WriteLine("Parsing was end \n");
                        DataWriter.Write(fullName, resultList);
                        break;
                    case UserControl.cpp:
                        info = "You choose all directories with .cpp file";
                        Console.WriteLine(info + "\n");
                        pp.GetCCPPathAsync(path).GetAwaiter().GetResult();
                        resultList = pp.Result;
                        Console.WriteLine("Parsing was end \n");
                        DataWriter.Write(fullName, resultList);
                        break;
                    case UserControl.r1:
                        info = "You choose all directories with reverse1 command";
                        Console.WriteLine(info + "\n");
                        pp.GetAllPathAsync(path, true, false).GetAwaiter().GetResult();
                        resultList = pp.Result;
                        Console.WriteLine("Parsing was end \n");
                        DataWriter.Write(fullName, resultList);
                        break;
                    case UserControl.r2:
                        info = "You choose all directories with reverse2 command";
                        Console.WriteLine(info + "\n");
                        pp.GetAllPathAsync(path, false, true).GetAwaiter().GetResult();
                        resultList = pp.Result;
                        Console.WriteLine("Parsing was end \n");
                        DataWriter.Write(fullName, resultList);
                        break;
                    case UserControl.exit:
                        isExit = true;
                        break;
                    default: Console.WriteLine("Wrong command"); break;
                }
            }
        }

        static void Instruction()
        {
            Console.WriteLine("____________________________________________");
            Console.WriteLine("Enter path: ");
            path = Console.ReadLine();
            Console.WriteLine("Enter command (all, cpp, r1 or r2) or exit");
            command = Console.ReadLine();
            return;
        }

    }
}
