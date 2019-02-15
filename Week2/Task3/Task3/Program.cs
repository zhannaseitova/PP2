using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3
{
    class Program
    {

        static void DisplayAll(FileSystemInfo el, int tabs)
        {
            string Tab = new string('\t', tabs);
            if (el.GetType() == typeof(FileInfo))
            {
                Console.WriteLine(Tab + el.Name);
                return;
            }
            Console.WriteLine(Tab + el.Name);
            DirectoryInfo DI = (DirectoryInfo)el;
            FileSystemInfo[] arr = DI.GetFileSystemInfos();
            foreach (FileSystemInfo El in arr)
            {
                DisplayAll(El, tabs + 1);
            }
        }

        static void Main(string[] args)
        {
            string path = Console.ReadLine();
            try
            {
                DirectoryInfo Dir = new DirectoryInfo(path);
                FileSystemInfo[] files = Dir.GetFileSystemInfos();
                foreach (FileSystemInfo el in files)
                {
                    DisplayAll(el, 1);
                }
            }
            catch (IOException)
            {
                Console.WriteLine("ERROR");
            }
            Console.ReadLine();
        }
    }
}
