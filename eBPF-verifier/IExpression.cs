using System;
namespace eBPF_verifier
{
	public interface IEdgeExpression
	{
		EdgeExpression GetAbstractExpression();
	}
}

