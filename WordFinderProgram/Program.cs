using System;
using System.Collections.Generic;

namespace WordFinderProgram
{
	class Program
	{
		static void Main(string[] args)
		{
			try
			{
                Console.WriteLine("Enter the matrix, line by line (press Enter after each line, leave blank to finish):");
                var matrix = new List<string>();
                while (true)
                {
                    var line = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(line))
                        break;

                    matrix.Add(line);
                }

                var wordFinder = new WordFinder(matrix);

                Console.WriteLine("Enter the words of the wordstream separated by spaces:");
                var input = Console.ReadLine();

                var wordstream = input.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                Console.WriteLine();

                var result = wordFinder.Find(wordstream);

                Console.WriteLine("Found words:");
                foreach (var word in result)
                {
                    Console.WriteLine(word);
                }
            }
			catch (Exception ex)
			{
                Console.WriteLine("ERROR!");
                Console.WriteLine("Error Message: " + ex.Message);
			}

            Console.WriteLine();
            Console.WriteLine("Press any key to exit.");
            Console.Read();
		}
	}
}
