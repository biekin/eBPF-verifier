using System;
namespace eBPF_verifier
{
	public class Equation
	{
		private ProgramPoint ProgramPoint;

		private AbstractExpression AbstractExpression;

		private AbstractState AbstractState;

		public Equation(ProgramPoint programPoint, AbstractExpression abstractExpression, AbstractState abstractState)
		{
			ProgramPoint = programPoint;
			AbstractExpression = abstractExpression;
			AbstractState = abstractState;
		}
	}
}

