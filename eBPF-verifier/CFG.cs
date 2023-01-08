using System;
using System.Text;
namespace eBPF_verifier
{
	public class CFG
	{
		public List<ProgramPoint> Nodes = new List<ProgramPoint>();
		public List<ICFGEdge> Edges = new List<ICFGEdge>();

		public CFG() { }

		public bool IsValid()
		{
			return true;
		}

		public void AddNode(ProgramPoint node)
		{
			Nodes.Add(node);
			node.Cfg = this;
        }

        public void AddEdge(ICFGEdge edge)
		{
			Edges.Add(edge);
		}

        public override string ToString()
        {
			var sb = new StringBuilder();
			sb.Append($"{Nodes.Count} Nodes: \n");
			foreach(var n in Nodes)
			{
				sb.Append(n.ToString() + "\n");
			}

			sb.Append($"\n{Edges.Count} Edges:\n");

            foreach (var e in Edges)
            {
                sb.Append(e.ToString() + "\n");
            }

			return sb.ToString();
        }
    }
}

