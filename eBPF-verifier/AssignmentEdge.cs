using System;
namespace eBPF_verifier
{
	public class AssignmentEdge : ICFGEdge
	{
		private IProgramVariable ProgramVariable;

        // TODO support assignment of single variable / literal
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

        public override string ToString()
        {
            return $"Edge: {From} --> {To}; {ProgramVariable} = {Expression}";
        }
    }
}

