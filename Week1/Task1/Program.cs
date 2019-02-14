using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1Task1
{
    class Program
    {
        static void Main(string[] args)
        {




            int[] arr = new int[int.Parse((Console.ReadLine()))]; //create an array, read the size from the console and set the array
            string[] numbers = Console.ReadLine().Split(); //read all elements from the console
            arr = Array.ConvertAll(numbers, int.Parse); // convert elements of string to the integers
          




            int emptyValues = 0;  //count number of prime numbers
            string array_2 = ""; //the output

            bool check = false;
            for (int i = 0; i < arr.Length; i++)  //check weather the number is prime 
            {

                
                    check = false;

                    if (arr[i] == 1)   // 1 is not a prime number, so the program excludes it
                        continue;
                    else
                        for (int k = 2; k < arr[i]; k++)
                            if (arr[i] % k == 0)  //prime number is a number that is divisible only by itself and 1
                            {
                                check = true;
                                

                            }



                    if (!check)
                    {
                    emptyValues++;
                        array_2 += (arr[i] + " ");
                    }

                 
            }

          
                Console.WriteLine(emptyValues);

                
                    Console.WriteLine(array_2);
                

                Console.ReadLine();
            
        }
    }
}
