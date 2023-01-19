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

        public IProgramVariable GetProgramVariable()
        {
            return Condition.ProgramVariable;
        }

        public override string ToString()
        {
            return $"Edge: {From} --> {To}; {Condition}";
        }

        public EdgeExpression GetEdgeExpression()
        {
            return new EdgeExpression(GetProgramVariable(), Condition);
        }
    }
}

