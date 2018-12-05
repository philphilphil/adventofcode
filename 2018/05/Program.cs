using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _05
{
    class Program
    {
        static void Main(string[] args)
        {
            char[] inputFile = File.ReadAllText(Directory.GetCurrentDirectory() + @"\input.txt").ToCharArray();

            int answer1 = ReactPolymer(inputFile);

            Console.WriteLine(answer1);
            Console.Read();
        }

        private static int ReactPolymer(char[] inputFile)
        {
            int i = 0;
            int endOfFile = inputFile.Length;
            while (true)
            {
                //if entire file was searched and none was found we are done
                if (i != 0 && i == inputFile.Length - 1)
                    break;

                var currentChar = inputFile[i];
                var nextChar = inputFile[i + 1];

                //If upper, check if next Char is same char in lower
                if (Char.IsUpper(currentChar))
                {
                    if (Char.IsLower(nextChar) && Char.ToUpper(nextChar) == currentChar)
                    {
                        //match found, remove both chars
                        inputFile = inputFile.Where((val, id) => id != i && id != (i + 1)).ToArray();
                        i = 0;
                        continue;
                    }
                }

                //other way arround
                if (Char.IsLower(currentChar))
                {
                    if (Char.IsUpper(nextChar) && Char.ToLower(nextChar) == currentChar)
                    {
                        //match found, remove both chars
                        inputFile = inputFile.Where((val, id) => id != i && id != (i + 1)).ToArray();
                        i = 0;
                        continue;
                    }

                }
                i++;
            }

            return inputFile.Length;
        }
    }
}
