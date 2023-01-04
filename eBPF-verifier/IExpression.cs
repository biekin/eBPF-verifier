using System;
namespace eBPF_verifier
{
	public interface IExpression
	{
		AbstractExpression GetAbstractExpression();
	}
}

