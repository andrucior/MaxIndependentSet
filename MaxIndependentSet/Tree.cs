namespace MaxIndependentSet
{
    public class Edge<T>(Vertex<T> from, Vertex<T> to)
    {
        public Vertex<T> From { get; } = from;
        public Vertex<T> To { get; } = to;
    }

    public class Vertex<T>(T value)
    {
        public T Value { get; } = value;

        public override string ToString() => Value?.ToString() ?? "null";
    }

    public class Tree<T>
    {
        private readonly List<Edge<T>> _edges = [];
        private readonly HashSet<Vertex<T>> _vertices = [];

        public IReadOnlyList<Edge<T>> Edges => _edges;
        public IReadOnlyCollection<Vertex<T>> Vertices => _vertices;

        public Vertex<T>? Root { get; private set; }

        public Tree(Vertex<T> root)
        {
            Root = root;
            _vertices.Add(root);
        }

        public void AddEdge(Vertex<T> from, Vertex<T> to)
        {
            if (HasPath(to, from))
                throw new InvalidOperationException("Dodanie tej krawędzi tworzy cykl.");

            var edge = new Edge<T>(from, to);

            _vertices.Add(from);
            _vertices.Add(to);

            _edges.Add(edge);
        }

        public IEnumerable<Vertex<T>> GetChildren(Vertex<T> v)
        {
            return _edges
                .Where(e => e.From.Equals(v))
                .Select(e => e.To);
        }

        public bool Contains(Vertex<T> v) => _vertices.Contains(v);

        public bool HasPath(Vertex<T> start, Vertex<T> target)
        {
            var visited = new HashSet<Vertex<T>>();
            return DFS(start, target, visited);
        }

        private bool DFS(Vertex<T> current, Vertex<T> target, HashSet<Vertex<T>> visited)
        {
            if (current.Equals(target))
                return true;

            visited.Add(current);

            foreach (var neighbor in GetChildren(current))
            {
                if (!visited.Contains(neighbor))
                {
                    if (DFS(neighbor, target, visited))
                        return true;
                }
            }

            return false;
        }

        public IEnumerable<Vertex<T>> BFS()
        {
            if (Root == null)
                yield break;

            var queue = new Queue<Vertex<T>>();
            var visited = new HashSet<Vertex<T>>();

            queue.Enqueue(Root);
            visited.Add(Root);

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                yield return current;

                foreach (var neighbor in GetChildren(current))
                {
                    if (visited.Add(neighbor))
                    {
                        queue.Enqueue(neighbor);
                    }
                }
            }
        }

        public IEnumerable<Vertex<T>> DFS()
        {
            if (Root == null)
                yield break;

            var visited = new HashSet<Vertex<T>>();
            var stack = new Stack<Vertex<T>>();

            stack.Push(Root);

            while (stack.Count > 0)
            {
                var current = stack.Pop();

                if (visited.Contains(current))
                    continue;

                visited.Add(current);
                yield return current;

                foreach (var neighbor in GetChildren(current))
                    stack.Push(neighbor);
            }
        }

        public void PrintTree()
        {
            foreach (var edge in Edges)
            {
                Console.WriteLine($"{edge.From.Value} -> {edge.To.Value}");
            }
        }

        public long CountMaxIndependentSets()
        {
            var dp = new Dictionary<Vertex<T>, (long x, long y, long z)>();
            var visited = new HashSet<Vertex<T>>();

            PostOrder(Root!, dp, visited);

            var rootVal = dp[Root!];
            return rootVal.x + rootVal.y;
        }

        private void PostOrder(
            Vertex<T> v,
            Dictionary<Vertex<T>, (long x, long y, long z)> dp,
            HashSet<Vertex<T>> visited)
        {
            visited.Add(v);

            var children = GetChildren(v).ToList();

            foreach (var c in children)
            {
                if (!visited.Contains(c))
                    PostOrder(c, dp, visited);
            }

            if (children.Count == 0)
            {
                dp[v] = (1, 0, 1);
                return;
            }

            if (children.All(c => GetChildren(c).Any() == false))
            {
                dp[v] = (1, 1, 0);
                return;
            }

            long zv = 1;
            foreach (var u in children)
            {
                zv *= dp[u].y;
            }

            long yv = 1;
            foreach (var u in children)
            {
                yv *= (dp[u].x + dp[u].y);
            }
            yv -= zv;

            long xv = 1;
            foreach (var u in children)
            {
                xv *= (dp[u].y + dp[u].z);
            }

            dp[v] = (xv, yv, zv);
        }
    }
}