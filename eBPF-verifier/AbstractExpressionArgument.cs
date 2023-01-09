using System;
namespace eBPF_verifier
{
	public class AbstractExpressionArgument
	{
		private ProgramPoint ProgramPointFrom;
		private EdgeExpression EdgeExpression;

		public AbstractExpressionArgument(ProgramPoint programPoint, EdgeExpression edgeExpression)
		{
			ProgramPointFrom = programPoint;
			EdgeExpression = edgeExpression;
		}
	}
}

