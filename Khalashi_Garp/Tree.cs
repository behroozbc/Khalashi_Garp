using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khalashi_Garp
{
    public class Tree
    {
        private readonly IList<Tuple<uint, uint>> _items;
        public Tree()
        {
            _items = new List<Tuple<uint, uint>>();
        }
        public void AddEdge(uint start,uint end)
        {
            AddEdge(new Tuple<uint, uint>(start, end));
        }
        public void AddEdge(Tuple<uint, uint> edge)
        {
            if (edge.Item1 == edge.Item2)
                throw new Exception("round isnt valid for tree");
            _items.Add(edge);
        }
        public Prufer ToPruer()
        {
            throw new NotImplementedException();
            var n = _items.Count;
            if (n < 2)
            {
                var msg = "Prüfer sequence undefined for trees with fewer than two nodes";
                throw new Exception(msg);
            }
            
        }
        public override string ToString()
        {
            var sb = new StringBuilder();
            foreach (var item in _items)
            {
                sb.Append($"({item.Item1},{item.Item2}) ");
            }
            return sb.ToString();
        }
    }
}
