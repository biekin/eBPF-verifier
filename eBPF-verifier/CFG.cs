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
	}
}

