using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    class Program
    {

        static bool IsPrime(int a)
        {
            if (a == 1)
            {
                return false;
            }
            for (int i = 2; i <= Math.Sqrt(a); i++)
            {
                if (a % i == 0)
                {
                    return false;
                }
            }
            return true;
        }

        static void Main(string[] args)
        {
            using (StreamReader sr = new StreamReader(@"C:\Users\Дина\Desktop\PP2\Week2\Task1\input.txt"))
            {
                string s = sr.ReadToEnd();
                int[] arr = Array.ConvertAll<string, int>(s.Split(), int.Parse);
                List<int> primeArr = new List<int>(arr.Length);
                foreach (int element in arr)
                {
                    if (IsPrime(element))
                    {
                        primeArr.Add(element);
                    }
                }
                using (StreamWriter sw = new StreamWriter(@"C:\Users\Дина\Desktop\PP2\Week2\Task1\output.txt"))
                {
                    foreach (int el in primeArr)
                    {
                        sw.Write(el + " ");
                    }
                }
            }
        }
    }
}
