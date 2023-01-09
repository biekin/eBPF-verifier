using System;
namespace eBPF_verifier
{
	public class AbstractExpression
	{
		private List<AbstractExpressionArgument> Arguments = new List<AbstractExpressionArgument>();

		public AbstractExpression() { }

		public void AddArgumnt(AbstractExpressionArgument arg)
		{
			Arguments.Add(arg);
		}
	}
}

