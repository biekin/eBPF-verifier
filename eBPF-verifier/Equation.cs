using System;
namespace eBPF_verifier
{
	public class Equation
	{
		private ProgramPoint ProgramPoint;
        private AbstractState AbstractState;
        private AbstractExpression AbstractExpression;

		public Equation(ProgramPoint programPoint, AbstractState abstractState, AbstractExpression abstractExpression)
		{
			ProgramPoint = programPoint;
			AbstractState = abstractState;
            AbstractExpression = abstractExpression;
		}
	}
}

