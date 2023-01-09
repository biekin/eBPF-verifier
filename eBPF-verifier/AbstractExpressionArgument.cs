using System;
namespace eBPF_verifier
{
	public class AbstractExpressionArgument
	{
		private ProgramPoint ProgramPointFrom;
		private IProgramExpression EdgeExpression;

		public AbstractExpressionArgument(ProgramPoint programPoint, IProgramExpression edgeExpression)
		{
			ProgramPointFrom = programPoint;
			EdgeExpression = edgeExpression;
		}
	}
}

