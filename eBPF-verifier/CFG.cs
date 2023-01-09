using System;
using System.Text;
namespace eBPF_verifier
{
	public class CFG
	{
		public List<ProgramPoint> Nodes = new List<ProgramPoint>();
		public List<ICFGEdge> Edges = new List<ICFGEdge>();
		public HashSet<IProgramVariable> Variables = new HashSet<IProgramVariable>();

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
			Variables.Add(edge.GetProgramVariable());
		}

		public AbstractState GetBlankAbstractState()
		{
			var blankAbstractState = new AbstractState();
			foreach (var v in Variables)
			{
				blankAbstractState.AddVariable(v);
			}
			return blankAbstractState;
		}

        public override string ToString()
        {
			var sb = new StringBuilder();
			sb.Append($"{Nodes.Count} Node(s): \n");
			foreach(var n in Nodes)
			{
				sb.Append(n.ToString() + "\n");
			}

			sb.Append($"\n{Edges.Count} Edge(s):\n");
            foreach (var e in Edges)
            {
                sb.Append(e.ToString() + "\n");
            }

			sb.Append($"\n{Variables.Count} Variables(s):\n");
			foreach (var v in Variables)
			{
				sb.Append(v.ToString() + "\n");
			}

			return sb.ToString();
        }
    }
}

