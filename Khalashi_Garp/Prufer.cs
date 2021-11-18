using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khalashi_Garp
{
    public class Prufer
    {
        public uint[] Sequence { get => _sequence; }
        private readonly uint[] _sequence;

        public Prufer(uint[] sequence)
        {
            this._sequence = sequence;
        }
        public Tree ToTree()
        {
            var tree = new Tree();
            var vertices = _sequence.Length + 2;
            var vertex_set = new int[vertices];

            // Initialize the array of vertices
            for (int i = 0; i < vertices; i++)
                vertex_set[i] = 0;

            // Number of occurrences of vertex in code
            for (int i = 0; i < vertices - 2; i++)
                vertex_set[_sequence[i] - 1] += 1;
            int j = 0;
            // Find the smallest label not present in
            // prufer[].
            for (int i = 0; i < vertices - 2; i++)
            {
                for (j = 0; j < vertices; j++)
                {

                    // If j + 1 is not present in prufer set
                    if (vertex_set[j] == 0)
                    {

                        // Remove from Prufer set and print
                        // pair.
                        vertex_set[j] = -1;
                        tree.AddEdge((uint)(j + 1), _sequence[i]);

                        vertex_set[_sequence[i] - 1]--;

                        break;
                    }
                }
            }
            j = 0;
            var lastItem = new Tuple<uint, uint>(0, 0);
            // For the last element
            for (int i = 0; i < vertices; i++)
            {
                if (vertex_set[i] == 0 && j == 0)
                {

                    lastItem = new Tuple<uint, uint>((uint)(i + 1), 0);
                    j++;
                }
                else if (vertex_set[i] == 0 && j == 1)
                    lastItem = new Tuple<uint, uint>(lastItem.Item1, (uint)(i + 1));
            }
            tree.AddEdge(lastItem);
            return tree;
        }
        public void PrintTreeEdges()
        {
            Console.Write("\nThe edge set E(G) is:\n");
            Console.WriteLine(ToTree().ToString());
        }
        public override string ToString()
        {
            return "["+ String.Join(',',_sequence)+"]";
        }
    }
}
