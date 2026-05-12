namespace MaxIndependentSet
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	public class Vertex
	{
		public int Id { get; }
		public List<Vertex> Children { get; } = new List<Vertex>();

		public Vertex(int id)
		{
			Id = id;
		}

		public override string ToString() => Id.ToString();
	}

	public class Tree
	{
		public Vertex[] Vertices { get; }
		public Vertex Root => Vertices[0];

		public Tree(int n)
		{
			Vertices = new Vertex[n];
			for (int i = 0; i < n; i++)
			{
				Vertices[i] = new Vertex(i);
			}
		}

		public void AddEdge(int parent, int child)
		{
			// Zakładamy, że parent < child, zgodnie z dokumentacją
			Vertices[parent].Children.Add(Vertices[child]);
		}

		public void PrintTree()
		{
			foreach (var v in Vertices)
			{
				foreach (var child in v.Children)
				{
					Console.WriteLine($"{v.Id} -> {child.Id}");
				}
			}
		}

		public long CountMaxIndependentSets()
		{
			int n = Vertices.Length;
			// Tablica -  dostęp (O(1))
			var dp = new (long x, long y, long z)[n];

			// od dołu do góry drzewa
			for (int i = n - 1; i >= 0; i--)
			{
				var v = Vertices[i];
				var children = v.Children;

				// Liść
				if (children.Count == 0)
				{
					dp[i] = (1, 0, 1);
					continue;
				}

				// Wierzchołek, którego dziećmi są wyłącznie liście
				if (children.All(c => c.Children.Count == 0))
				{
					dp[i] = (1, 1, 0);
					continue;
				}

				// Wewnątrz drzewa
				long zv = 1;
				foreach (var u in children)
				{
					zv *= dp[u.Id].y;
				}

				long yv = 1;
				foreach (var u in children)
				{
					yv *= (dp[u.Id].x + dp[u.Id].y);
				}
				yv -= zv;

				long xv = 1;
				foreach (var u in children)
				{
					xv *= (dp[u.Id].y + dp[u.Id].z);
				}

				dp[i] = (xv, yv, zv);
			}

			return dp[0].x + dp[0].y;
		}
	}
}