using System;
namespace eBPF_verifier
{
	public class Equation
	{
		private ProgramPoint ProgramPoint;
        private AbstractExpression AbstractExpression;

		public Equation(ProgramPoint programPoint, AbstractExpression abstractExpression)
		{
			ProgramPoint = programPoint;
            AbstractExpression = abstractExpression;
		}

        public override string ToString()
        {
            return $"{ProgramPoint} := {AbstractExpression}\n";
        }
    }
}

