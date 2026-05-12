namespace MaxIndependentSet
{
	using System;
	using System.IO;

	public static class TreeLoader
	{
		public static Tree LoadTree(string filename)
		{
			using StreamReader reader = new(Path.Combine(GetDataFolder(), filename));

			int n = int.Parse(reader.ReadLine()!);

			// Inicjalizujemy drzewo na n elementów
			var tree = new Tree(n);

			for (int i = 0; i < n - 1; i++)
			{
				var line = reader.ReadLine()!;
				var parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);

				int u = int.Parse(parts[0]);
				int v = int.Parse(parts[1]);

				// Wymuszamy, by rodzic miał mniejszy indeks, dodajemy krawędź
				tree.AddEdge(Math.Min(u, v), Math.Max(u, v));
			}

			return tree;
		}

		public static void GenerateTestData()
		{
			for (int i = 0; i < 10; i++)
			{
				CreateRandomTreeFile($"input_{i}.txt", 1 + new Random().Next(20));
			}
		}

		public static void CreateRandomTreeFile(string filename, int n)
		{
			using StreamWriter writer = new(Path.Combine(GetDataFolder(), filename));
			Random rand = new();

			writer.WriteLine(n);

			for (int i = 1; i < n; i++)
			{
				int parent = rand.Next(0, i);
				writer.WriteLine($"{parent} {i}");
			}
		}

		public static string GetDataFolder()
		{
			var baseDir = AppContext.BaseDirectory;
			var projectDir = Directory.GetParent(baseDir)!.Parent!.Parent!.Parent!.FullName;
			var dataDir = Path.Combine(projectDir, "Data");
			Directory.CreateDirectory(dataDir);
			return dataDir;
		}
	}
}