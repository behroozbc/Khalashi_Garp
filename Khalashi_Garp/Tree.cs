using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khalashi_Garp;
public class Tree
{
    private readonly IList<Tuple<uint, uint>> _edgeItems;
    /// <summary>
    /// item1 =>node name
    /// item2=>degree
    /// </summary>
    private readonly IDictionary<uint, int> _nodeItems;
    public Tree()
    {
        _edgeItems = new List<Tuple<uint, uint>>();
        _nodeItems = new Dictionary<uint, int>();
    }
    /// <summary>
    /// Add edge by node label start and end
    /// </summary>
    /// <param name="start">first node</param>
    /// <param name="end">end node</param>
    /// <exception cref="Exception"></exception>
    public void AddEdge(uint start, uint end)
    {
        AddEdge(new Tuple<uint, uint>(start, end));
    }
    /// <summary>
    /// Add Edge with Tuple 
    /// </summary>
    /// <param name="edge">Edge cordinate</param>
    /// <exception cref="Exception">When Edge is round  </exception>
    public void AddEdge(Tuple<uint, uint> edge)
    {
        if (edge.Item1 == edge.Item2)
            throw new Exception("round isnt valid for tree");
        /// <summary>
        /// Count node degree 
        /// If Node is new create
        /// </summary>
        /// <param name="node"></param>
        void addDegreeToNode(uint node)
        {
            if (!_nodeItems.ContainsKey(node))
                _nodeItems.Add(node, 1);
            else
                _nodeItems[node]++;
        }
        addDegreeToNode(edge.Item1);
        addDegreeToNode(edge.Item2);
        _edgeItems.Add(edge);
    }
    public Prufer ToPruer()
    {
        var n = _nodeItems.Count;
        if (n < 2)
        {
            var msg = "Prüfer sequence undefined for trees with fewer than two nodes";
            throw new Exception(msg);
        }
        var nodes = new Dictionary<uint, int>(_nodeItems);
        var edges = new List<Tuple<uint, uint>>(_edgeItems);
        uint parents(uint node)
        {
            return Adjacency(node).First(c => nodes[c] > 1);
        }
        uint getNextItem(uint index)
        {
            for (uint i = index+1; i < n; i++)
            {
                if(nodes[i]==1)
                    return i;
            }
            return index;
        }
        uint index,
            u = _nodeItems.First(c => c.Value == 1).Key;
        index = u;
        var result=new List<uint>();
        for(int i = 0; i < n-2; i++)
        {
            var v = parents(u);
            result.Add(v);
            nodes[v]-=1;
            if (v < index && nodes[v] == 1)
                u = v;
            else
                index=u=getNextItem(index);
        }
        return new Prufer(result.ToArray());
    }
    public override string ToString()
    {
        var sb = new StringBuilder();
        foreach (var item in _edgeItems)
        {
            sb.Append($"({item.Item1},{item.Item2}) ");
        }
        return sb.ToString();
    }
    public uint GetDegreeOfNode(uint node)
    {
        return (uint)_edgeItems.Where(n => n.Item1 == node || n.Item2 == node).Count();
    }
    public IEnumerable<uint> Adjacency(uint node)
    {
        return _edgeItems.Where(c => c.Item1 == node || c.Item2 == node).Select(c =>c.Item1==node? c.Item2 : c.Item1);
    }
}

