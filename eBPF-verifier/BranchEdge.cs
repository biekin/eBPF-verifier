using System;
using System.Linq.Expressions;

namespace eBPF_verifier
{
	public class BranchEdge : ICFGEdge
	{
		public ICFGNode From { get; private set; }

		public ICFGNode To { get; private set;  }

		private Condition Condition;

		public BranchEdge(ICFGNode from, ICFGNode to, Condition condition)
		{
			From = from;
			To = to;
			Condition = condition;
		}

        public AbstractExpression GetAbstractExpresison()
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return $"Edge: {From} --> {To}; {Condition}";
        }
    }
}

