using System;
namespace eBPF_verifier
{
	public class Equation
	{
		public ProgramPoint ProgramPoint { get; private set; }
        private AbstractExpression AbstractExpression;

		public Equation(ProgramPoint programPoint, AbstractExpression abstractExpression)
		{
			ProgramPoint = programPoint;
            AbstractExpression = abstractExpression;
		}

		public void Update()
		{
			foreach (var arg in AbstractExpression.Arguments)
			{
				var variable = arg.EdgeExpression.ProgramVariable;
				var abstractState = arg.ProgramPointFrom.AbstractState;
				var interval = arg.EdgeExpression.IntervalEvaluableExpression.GetInterval(abstractState);
				ProgramPoint.AbstractState.Update(variable, interval);
			}
		}

		public override string ToString()
        {
            return $"{ProgramPoint} := {AbstractExpression}\n";
        }
    }
}

