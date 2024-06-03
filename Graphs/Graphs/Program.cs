namespace Graphs
{
    public class Program
    {
        static void Main(string[] args)
        {
            Graph graph = new Graph(4, 4);
            graph.GraphPrint();
            graph.Dijkstra(0);
            Console.WriteLine("=================");
            graph.Dijkstra(1);
            Console.WriteLine("=================");
            graph.Dijkstra(2);
            Console.WriteLine("=================");
            graph.Dijkstra(3);
            Console.WriteLine("=================");
            Console.WriteLine("=================");
            Console.WriteLine("=================");
            graph.Kruskal();
            Console.WriteLine("=================");
            Console.WriteLine("=================");
            Console.WriteLine("=================");
            graph.Prim_SLL();
            Console.ReadLine();
        }
    }
}
