namespace MaxIndependentSet
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Numerics; // wielkie liczb

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

		
		public BigInteger CountMaxIndependentSets()
		{
			int n = Vertices.Length;

			
			var dp = new (BigInteger x, BigInteger y, BigInteger z)[n];

			for (int i = n - 1; i >= 0; i--)
			{
				var v = Vertices[i];
				var children = v.Children;

				if (children.Count == 0)
				{
					dp[i] = (1, 0, 1);
					continue;
				}

				if (children.All(c => c.Children.Count == 0))
				{
					dp[i] = (1, 1, 0);
					continue;
				}

				BigInteger zv = 1;
				foreach (var u in children)
				{
					zv *= dp[u.Id].y;
				}

				BigInteger yv = 1;
				foreach (var u in children)
				{
					yv *= (dp[u.Id].x + dp[u.Id].y);
				}
				yv -= zv;

				BigInteger xv = 1;
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