using System;
namespace eBPF_verifier
{
	public class AssignmentEdge : ICFGEdge
	{
		private IProgramVariable ProgramVariable;

        // TODO support assignment of single variable / literal
        private IEdgeExpression Expression;

        public ProgramPoint From { get; private set; }

        public ProgramPoint To { get; private set; }

        public AssignmentEdge(ProgramPoint from, ProgramPoint to, IProgramVariable programVariable, IEdgeExpression expression)
		{
            From = from;
            To = to;
            ProgramVariable = programVariable;
			Expression = expression;
		}

        public IEdgeExpression GetEdgeExpresison()
        {
            return Expression;
        }

        public IProgramVariable GetProgramVariable()
        {
            return ProgramVariable;
        }

        public override string ToString()
        {
            return $"Edge: {From} --> {To}; {ProgramVariable} := {Expression}";
        }
    }
}

