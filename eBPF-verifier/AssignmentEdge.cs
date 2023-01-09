using System;
namespace eBPF_verifier
{
	public class AssignmentEdge : ICFGEdge
	{
		private IProgramVariable ProgramVariable;

        // TODO support assignment of single variable / literal
        private IProgramExpression Expression;

        public ProgramPoint From { get; private set; }

        public ProgramPoint To { get; private set; }

        public AssignmentEdge(ProgramPoint from, ProgramPoint to, IProgramVariable programVariable, IProgramExpression expression)
		{
            From = from;
            To = to;
            ProgramVariable = programVariable;
			Expression = expression;
		}

        public IProgramExpression GetEdgeExpresison()
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

        public EdgeExpression GetEdgeExpression()
        {
            throw new NotImplementedException();
        }
    }
}

