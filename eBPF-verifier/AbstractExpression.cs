using System;
namespace eBPF_verifier
{
	public class AbstractExpression : IAbstractExpressionArgument
	{
		private IAbstractExpressionArgument Arg1;
		private IAbstractExpressionArgument Arg2;

		public AbstractExpression(IAbstractExpressionArgument arg1, IAbstractExpressionArgument arg2)
		{
			Arg1 = arg1;
			Arg2 = arg2;
		}
	}
}

