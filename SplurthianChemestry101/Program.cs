using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SplurthianChemestry101
{
    class Program
    {
        static int Main(string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine("Invalid input, two arguments required.");
                Console.WriteLine("Element Name: <string>");
                Console.WriteLine("Element Symbol: <string>");

                return 1;
            }

            string name = args[0];
            string symbol = args[1];

            bool isSymbolValid = TestSymbol(name, symbol);

            Console.WriteLine(string.Format("{0}, {1} -> {2}", name, symbol, isSymbolValid));
            Console.WriteLine(string.Format("First alphabetical symbol: {0}", GetFirstAlphabeticalSymbol(name)));
            Console.WriteLine(string.Format("Number of distinct valid symbols: {0}", GetNumberOfDistinctSymbols(name)));
            Console.WriteLine(string.Format("Number of distinct valid symbols for Blurth: {0}", GetNumberOfDistinctSymbolsBlurth(name)));
            Console.ReadLine();

            return 0;
        }

        // Initial Challenge
        private static bool TestSymbol(string name, string symbol)
        {
            name = name.ToLower();
            symbol = symbol.ToLower();

            bool result = false;

            if (symbol.Length == 2)
            {
                int firstIndex = name.IndexOf(symbol[0]);

                if (firstIndex >= 0)
                    result = name.Substring(firstIndex).IndexOf(symbol[1]) >= 0;
            }

            return result;
        }

        // Optional 1
        private static string GetFirstAlphabeticalSymbol(string name)
        {
            name = name.ToLower();

            char firstLetter = name.Substring(0, name.Length - 2).OrderBy(x => x).First();
            char secondLetter = name.Substring(name.IndexOf(firstLetter) + 1).OrderBy(x => x).First();

            return new string(new char[] { char.ToUpper(firstLetter), secondLetter });
        }

        // Optional 2
        private static int GetNumberOfDistinctSymbols(string name)
        {
            return GetAllDistinctSymbols(name).Count();
        }

        // Optional 3
        private static int GetNumberOfDistinctSymbolsBlurth(string name)
        {
            return GetAllDistinctSymbolsBlurth(name).Count();
        }

        private static IEnumerable<string> GetAllDistinctSymbols(string name)
        {
            IEnumerable<string> symbols = new List<string>();

            if (name.Length > 1)
            {
                char first = char.ToUpper(name[0]);
                var newList = name.Substring(1).Select(x => new string(new char[] { first, x })).Distinct();
                symbols = newList.Union(GetAllDistinctSymbols(name.Substring(1)));
            }

            return symbols;
        }

        private static IEnumerable<string> GetAllDistinctSymbolsBlurth(string name)
        {
            if (name.Length == 1)
                return new List<string>() { name };
            else
            {
                var othersList = GetAllDistinctSymbolsBlurth(name.Substring(1));
                var thisList = othersList
                    .Union(othersList.Select(x => name[0] + x))
                    .Union(new List<string>() { name[0].ToString() });
                return thisList;
            }
        }
    }
}
