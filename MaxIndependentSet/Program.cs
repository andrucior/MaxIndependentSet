using MaxIndependentSet;
using System;
using System.IO;

class Program
{
    static void Main()
    {
        while (true)
        {
            Console.WriteLine("\n=== Max Independent Set ===");
            Console.WriteLine("1. Wybierz plik z folderu Data");
            Console.WriteLine("2. Losowe drzewo");
            Console.WriteLine("0. Wyjście");
            Console.Write("Twój wybór: ");

            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    RunFromFile();
                    break;

                case "2":
                    RunExample();
                    break;

                case "0":
                    return;

                default:
                    Console.WriteLine("Nieznana opcja.");
                    break;
            }
        }

        static void RunFromFile()
        {
            ShowFiles();

            Console.Write("Podaj nazwę pliku:");
            string file = Console.ReadLine()!;

            if (!File.Exists(Path.Combine(TreeLoader.GetDataFolder(), file)))
            {
                Console.WriteLine("Plik nie istnieje.");
                return;
            }

            Process(file);
        }

        static void ShowFiles()
        {
            var files = Directory.GetFiles(TreeLoader.GetDataFolder(), "input_*.txt");

            if (files.Length == 0)
            {
                Console.WriteLine("Brak plików input_*.txt");
                return;
            }

            Console.WriteLine("\nDostępne pliki:");
            foreach (var f in files)
                Console.WriteLine("- " + Path.GetFileName(f));
        }

        static void Process(string file)
        {
            Console.WriteLine($"\nPlik: {file}\n");

            Console.WriteLine("Wczytano drzewo:");
            var tree = TreeLoader.LoadTree(file);

            tree.PrintTree();

            long result = tree.CountMaxIndependentSets();

            Console.WriteLine($"\nWynik: {result}");
        }

        static void RunExample()
        {
            Console.WriteLine("Podaj rozmiar");
            var choice = Console.ReadLine();

            if (!int.TryParse(choice, out int parsedChoice))
            {
                Console.WriteLine("Rozmiar musi być liczbą całkowitą!");
                RunExample();
            }
            if (parsedChoice <= 0 || parsedChoice > 100)
            {
                Console.WriteLine("Rozmiar nieprawidlowy!");
                RunExample();
            }
            

            string temp = "example.txt";
            TreeLoader.CreateRandomTreeFile(temp, parsedChoice);
            Process(temp);
        }
    }
}