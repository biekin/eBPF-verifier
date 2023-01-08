using System;
namespace eBPF_verifier
{
	public class AbstractExpressionArgument
	{
		private AbstractState AbstractState;
		private IEdgeExpression EdgeExpression;

		public AbstractExpressionArgument(AbstractState abstractState, IEdgeExpression edgeExpression)
		{
			AbstractState = abstractState;
			EdgeExpression = edgeExpression;
		}
	}
}

