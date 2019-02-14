using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2
{
    class Program
    {
        static void Main(string[] args)
        {


        
            int[] a = new int[int.Parse(Console.ReadLine())]; // an array with a size equal to the number read from the console

            string[] nums = Console.ReadLine().Split(); // read numbers 
            a = Array.ConvertAll(nums, int.Parse);

            string res = string.Empty; // string for output


            
             

            for (int i = 0; i < a.Length; i++)
            {
                res += a[i] + " " + a[i] + " "; //duplicate numbers
            }

              
            Console.WriteLine(res); //print duplicated numbers

            Console.ReadLine();

        
        
        }
    }
}
