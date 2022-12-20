using System;
namespace eBPF_verifier
{
	public class BranchEdge : ICFGEdge
	{
		public ICFGNode From { get; private set; }
		public ICFGNode To { get; private set;  }

		public BranchEdge(ICFGNode from, ICFGNode to)
		{
			From = from;
			To = to;
		}

        public AbstractExpression GetAbstractExpresison()
        {
            throw new NotImplementedException();
        }
    }
}

