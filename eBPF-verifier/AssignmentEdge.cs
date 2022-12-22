using System;
namespace eBPF_verifier
{
	public class AssignmentEdge : ICFGEdge
	{
		private IProgramVariable ProgramVariable;

		private IExpression Expression;

        public ICFGNode From { get; private set; }

        public ICFGNode To { get; private set; }

        public AssignmentEdge(ICFGNode from, ICFGNode to, IProgramVariable programVariable, IExpression expression)
		{
            From = from;
            To = to;
            ProgramVariable = programVariable;
			Expression = expression;
		}

        public AbstractExpression GetAbstractExpresison()
        {
            throw new NotImplementedException();
        }
    }
}

