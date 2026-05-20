namespace MaxIndependentSet
{
	using System;
	using System.IO;

	public static class TreeLoader
	{
		public static Tree LoadTree(string filename)
		{
			using StreamReader reader = new(filename);

			int n = int.Parse(reader.ReadLine()!);

			var tree = new Tree(n);

			for (int i = 0; i < n - 1; i++)
			{
				var line = reader.ReadLine()!;
				var parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);

				int u = int.Parse(parts[0]);
				int v = int.Parse(parts[1]);

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
			// Zapis bezpośrednio do bieżącego katalogu
			using StreamWriter writer = new(filename);
			Random rand = new();

			writer.WriteLine(n);

			for (int i = 1; i < n; i++)
			{
				int parent = rand.Next(0, i);
				writer.WriteLine($"{parent} {i}");
			}
		}
	}
}