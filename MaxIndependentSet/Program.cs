using MaxIndependentSet;
using System;
using System.Diagnostics;
using System.IO;
using System.Numerics;

class Program
{
	static void Main(string[] args)
	{
		
		if (args.Length > 0)
		{
			var tree = TreeLoader.LoadTree(args[0]);

			Stopwatch sw = Stopwatch.StartNew();
			BigInteger result = tree.CountMaxIndependentSets();
			sw.Stop();

			Console.WriteLine(result);
			Console.WriteLine(sw.Elapsed.TotalMicroseconds);
			return;
		}

		
		while (true)
		{
			Console.WriteLine("\n=== Max Independent Set ===");
			Console.WriteLine("1. Wybierz plik z bieżącego katalogu");
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
	}

	static void RunFromFile()
	{
		ShowFiles();

		Console.Write("Podaj nazwę pliku: ");
		string file = Console.ReadLine()!;

		if (!File.Exists(file))
		{
			Console.WriteLine("Plik nie istnieje.");
			return;
		}

		Process(file);
	}

	static void ShowFiles()
	{
		var files = Directory.GetFiles(".", "input_*.txt");

		if (files.Length == 0)
		{
			Console.WriteLine("Brak plików input_*.txt w bieżącym katalogu.");
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

		//Stopwatch sw = Stopwatch.StartNew();
		BigInteger result = tree.CountMaxIndependentSets();
		//sw.Stop();

		Console.WriteLine($"\nWynik (MIS): {result}");
		//Console.WriteLine($"Czas czystych obliczeń: {sw.Elapsed.TotalMicroseconds:F2} µs");
	}

	static void RunExample()
	{
		Console.WriteLine("Podaj rozmiar:");
		var choice = Console.ReadLine();

		if (!int.TryParse(choice, out int parsedChoice) || parsedChoice <= 0 || parsedChoice > 10000)
		{
			Console.WriteLine("Rozmiar nieprawidłowy! Wymagana liczba całkowita z przedziału (0, 10000].");
			return;
		}

		string temp = "example.txt";
		TreeLoader.CreateRandomTreeFile(temp, parsedChoice);
		Process(temp);
	}
}