namespace Graphs;

public class Graph
{
    private int[,] Matrix { get; }
    private int MatrixSize { get; }
    public Graph(int x, int y)
    {
        MatrixSize = x;
        Matrix = new int[x, y];

        for (var i = 0; i < x; i++)
        {
            for (var j = 0; j < y; j++)
            {
                Console.Write($"Enter the weight of the node {i} and {j}: ");
                if (!int.TryParse(Console.ReadLine(), out var weight))
                {
                    Console.WriteLine("Incorrect input. Enter the number");
                    j--;
                }
                else
                {
                    Matrix[i, j] = weight;
                }
            }
        }
    }
    public void GraphPrint()
    {
        Console.WriteLine("Your graph : ");
        Console.WriteLine();
        for (var i = 0; i < Matrix.GetLength(0); i++)
        {
            for (var j = 0; j < Matrix.GetLength(1); j++)
            {
                Console.Write($"{Matrix[i, j]} ");
            }
            Console.WriteLine();
        }
        Console.WriteLine();
    }
    private int MinDistance(int[] dist, bool[] sptSet)
    {
        var min = int.MaxValue;
        var minIndex = -1;

        for (var i = 0; i < MatrixSize; i++)
        {
            if (!sptSet[i] && dist[i] <= min)
            {
                min = dist[i];
                minIndex = i;
            }
        }

        return minIndex;
    }
    public void Dijkstra(int root)
    {
        var dist = new int[MatrixSize];

        var checkPoint = new bool[MatrixSize];

        for (var i = 0; i < MatrixSize; i++)
        {
            dist[i] = int.MaxValue;
            checkPoint[i] = false;
        }

        dist[root] = 0;

        for (var i = 0; i < MatrixSize - 1; i++)
        {

            var minDist = MinDistance(dist, checkPoint);

            checkPoint[minDist] = true;

            for (var j = 0; j < MatrixSize; j++)

                if (!checkPoint[j] && Matrix[minDist, j] != 0 && dist[minDist] != int.MaxValue && dist[minDist] + Matrix[minDist, j] < dist[j])
                    dist[j] = dist[minDist] + Matrix[minDist, j];
        }

        for (var i = 0; i < dist.Length; i++)
        {
            Console.WriteLine($"Distance for {i} root is {dist[i]}");
        }
    }
    public void Kruskal()
    {
        var edges = new List<(int, int, int)>();

        for (var i = 0; i < MatrixSize; i++)
        {
            for (var j = i + 1; j < MatrixSize; j++)
            {
                if (Matrix[i, j] != 0)
                {
                    edges.Add((i, j, Matrix[i, j]));
                }
            }
        }

        edges.Sort((a, b) => a.Item3.CompareTo(b.Item3));

        var tree = new int[MatrixSize];
        for (var i = 0; i < MatrixSize; i++)
        {
            tree[i] = i;
        }

        var minimumSpanningTree = new List<(int, int, int)>();

        foreach (var edge in edges)
        {
            var parent1 = Find(tree, edge.Item1);
            var parent2 = Find(tree, edge.Item2);

            if (parent1 != parent2)
            {
                minimumSpanningTree.Add(edge);
                tree[parent2] = parent1;
            }
        }

        Console.WriteLine("Minimum spanning tree (edge):");
        foreach (var edge in minimumSpanningTree)
        {
            Console.WriteLine($"({edge.Item1}, {edge.Item2}) weight: {edge.Item3}");
        }
    }
    public class Node
    {
        public int Vertex { get; set; }
        public int Weight { get; set; }
        public Node Next { get; set; }

        public Node(int vertex, int weight)
        {
            Vertex = vertex;
            Weight = weight;
            Next = null;
        }
    }

    protected class SortedLinkedList
    {
        private Node head = null;

        public void Insert(int vertex, int weight)
        {
            var newNode = new Node(vertex, weight);
            if (head == null || head.Weight > weight)
            {
                newNode.Next = head;
                head = newNode;
            }
            else
            {
                var current = head;
                while (current.Next != null && current.Next.Weight <= weight)
                {
                    current = current.Next;
                }
                newNode.Next = current.Next;
                current.Next = newNode;
            }
        }

        public Node ExtractMin()
        {
            if (head == null) return null;
            var minNode = head;
            head = head.Next;
            return minNode;
        }

        public bool IsEmpty()
        {
            return head == null;
        }
    }

    public void Prim_SLL()
    {
        var inMST = new bool[MatrixSize];
        var priorityQueue = new SortedLinkedList();
        var resultMST = new List<(int, int, int)>();

        priorityQueue.Insert(0, 0);

        while (!priorityQueue.IsEmpty())
        {
            var minNode = priorityQueue.ExtractMin();
            var minNodeVertex = minNode.Vertex;

            if (inMST[minNodeVertex])
            {
                continue;
            }

            inMST[minNodeVertex] = true;

            for (var i = 0; i < MatrixSize; i++)
            {
                if (Matrix[minNodeVertex, i] != 0 && !inMST[i])
                {
                    priorityQueue.Insert(i, Matrix[minNodeVertex, i]);
                    resultMST.Add((minNodeVertex, i, Matrix[minNodeVertex, i]));
                }
            }
        }

        Console.WriteLine("Minimum spanning tree (edge) using Prim:");
        foreach (var edge in resultMST)
        {
            Console.WriteLine($"({edge.Item1}, {edge.Item2}) weight: {edge.Item3}");
        }
    }

    private static int Find(int[] tree, int vertex)
    {
        while (tree[vertex] != vertex)
        {
            vertex = tree[vertex];
        }
        return vertex;
    }
}