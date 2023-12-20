using System;
using eBPF_verifier.Common;

namespace eBPF_verifier
{
	public class AbstractExpressionArgument
	{
		public ProgramPoint ProgramPointFrom;
		public EdgeExpression EdgeExpression;

		public AbstractExpressionArgument(ProgramPoint programPoint, EdgeExpression edgeExpression)
		{
			ProgramPointFrom = programPoint;
			EdgeExpression = edgeExpression;
		}

		public Interval GetInterval()
		{
			return EdgeExpression.GetInterval(ProgramPointFrom.AbstractState);
		}

		public TristateNumber GetTristateNumber()
		{
			return EdgeExpression.GetTristateNumber(ProgramPointFrom.AbstractState);
		}

		public override string ToString()
        {
			return $"⟦{EdgeExpression}⟧({ProgramPointFrom})";
        }
    }
}

