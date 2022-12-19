using System;
namespace eBPF_verifier
{
	public class AssignmentEdge
	{
		private IProgramVariable ProgramVariable;
		private IExpression Expression;

		public AssignmentEdge(IProgramVariable programVariable, IExpression expression)
		{
			ProgramVariable = programVariable;
			Expression = expression;
		}
	}
}

