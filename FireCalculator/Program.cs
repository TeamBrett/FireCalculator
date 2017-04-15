using System;
using System.IO;
using System.Linq;

namespace FireCalculator {
    public class Program {
        public static void Main(string[] args) {
            Strategy strategy;
            var file = "data.json";
            if (args?.FirstOrDefault().IsNullOrEmpty() == false) {
                file = args[0];
                strategy = File.ReadAllText(file).FromJson<Strategy>();
            } else {
                strategy = File.ReadAllText(file).FromJson<Strategy>();
                ////strategy = BuildStrategy.DefaultStrategy;
                ////File.WriteAllText(file, strategy.ToJson());
            }

            var line = string.Empty;
            do {
                Console.Clear();
                Console.WriteLine("How many Years?");
                if(!int.TryParse(Console.ReadLine(), out int years)) {
                    Console.WriteLine("Oops, I didn't quite understand that.");
                    continue;
                }

                strategy.StartDate = DateTime.Today;

                strategy.Run(DateTime.Now.AddYears(years));

                var assets = strategy.Assets.Sum(a => a.Value);
                var liabilities = strategy.Liabilities.Sum(a => a.Value);
                Console.WriteLine("Total Assets: ", assets);
                Console.WriteLine("Total Liabilities: ", liabilities);
                Console.WriteLine("Net Worth: ", assets - liabilities);
                Console.WriteLine("Asset Income per Year: ");
                line = Console.ReadLine();
            } while (line.ToLower().Trim() != "quit");
        }
    }
}