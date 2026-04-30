namespace MaxIndependentSet
{
    public static class TreeLoader
    {
        public static Tree<int> LoadTree(string filename)
        {
            using StreamReader reader = new(Path.Combine(GetDataFolder(), filename));

            int n = int.Parse(reader.ReadLine()!);

            var vertices = new Vertex<int>[n];
            for (int i = 0; i < n; i++)
            {
                vertices[i] = new Vertex<int>(i);
            }

            var tree = new Tree<int>(vertices[0]);

            for (int i = 0; i < n - 1; i++)
            {
                var line = reader.ReadLine()!;
                var parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                int u = int.Parse(parts[0]);
                int v = int.Parse(parts[1]);

                //  u < v -> u = parent
                tree.AddEdge(vertices[u], vertices[v]);
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