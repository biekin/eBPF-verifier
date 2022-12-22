using System;
namespace eBPF_verifier
{
	public class CFG
	{
		private List<ICFGNode> Nodes = new List<ICFGNode>();
		private List<ICFGEdge> Edges = new List<ICFGEdge>();

		public CFG() { }

		public bool IsValid()
		{
			return true;
		}

		public void AddNode(ICFGNode node)
		{
			Nodes.Append(node);
		}

		public void AddEdge(ICFGEdge edge)
		{
			Edges.Append(edge);
		}
	}
}

