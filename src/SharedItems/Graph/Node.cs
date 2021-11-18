using SharedItems.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedItems.Graph
{
    public class Node : ValueObject
    {
        public Node(object name)
        {
            Name = name;
        }

        public object Name { get; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Name;
        }
    }
}
