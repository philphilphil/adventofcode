using System;
using System.IO;

namespace _01
{
    class Program
    {
        static void Main(string[] args)
        {
            int currentFrequency = 0;
            string line;

            var fileLocation = System.Reflection.Assembly.GetEntryAssembly().Location;


            StreamReader inputFile = new StreamReader(Directory.GetCurrentDirectory() + @"\input.txt");  
            while((line = inputFile.ReadLine()) != null)  
            {  
                int frequencyChange = int.Parse(line); 
                currentFrequency += frequencyChange;
            } 

            Console.WriteLine(currentFrequency.ToString());
            Console.Read();
        }
    }
}
