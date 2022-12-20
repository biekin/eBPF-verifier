using System;
namespace eBPF_verifier
{
	public class AssignmentEdge : ICFGEdge
	{
		private IProgramVariable ProgramVariable;

		private IExpression Expression;

        public ICFGNode From => throw new NotImplementedException();

        public ICFGNode To => throw new NotImplementedException();

        public AssignmentEdge(IProgramVariable programVariable, IExpression expression)
		{
			ProgramVariable = programVariable;
			Expression = expression;
		}

        public AbstractExpression GetAbstractExpresison()
        {
            throw new NotImplementedException();
        }
    }
}

