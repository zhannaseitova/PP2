using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Task4
{
    class Program
    {
        static void Main(string[] args)
        {
            FileInfo newF = new FileInfo(@"C:\Users\Дина\Desktop\PP2\Week2\Task 4\My text.txt");
            DirectoryInfo dir = new DirectoryInfo(@"C:\Users\Дина\Desktop\PP2\Week2\Task 4\My Directory");
            if (!dir.Exists)
            {
                dir.Create();
                FileInfo newNew = new FileInfo(dir.FullName + @"\Me text2.txt");
                using (FileStream fs = newF.OpenWrite())
                {
                    string text = "From the new directory";
                    fs.Write(Encoding.Default.GetBytes(text), 0, Encoding.Default.GetByteCount(text));
                }
                newF.CopyTo(newNew.FullName);
                newF.Delete();
            }
            else
            {
                dir.Delete(true);
            }
        }
    }
}