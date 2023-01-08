using System;
using System.Linq.Expressions;

namespace eBPF_verifier
{
	public class BranchEdge : ICFGEdge
	{
		public ProgramPoint From { get; private set; }

		public ProgramPoint To { get; private set;  }

		private Condition Condition;

		public BranchEdge(ProgramPoint from, ProgramPoint to, Condition condition)
		{
			From = from;
			To = to;
			Condition = condition;
		}

        public IEdgeExpression GetEdgeExpresison()
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return $"Edge: {From} --> {To}; {Condition}";
        }
    }
}

