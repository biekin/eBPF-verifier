using System;
namespace eBPF_verifier
{
	public class AbstractExpressionArgument
	{
		private ProgramPoint ProgramPointFrom;
		private IEdgeExpression EdgeExpression;

		public AbstractExpressionArgument(ProgramPoint programPoint, IEdgeExpression edgeExpression)
		{
			ProgramPointFrom = programPoint;
			EdgeExpression = edgeExpression;
		}
	}
}

