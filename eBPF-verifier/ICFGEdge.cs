using System;
namespace eBPF_verifier
{
	public interface ICFGEdge
	{
		ICFGNode From { get; }
		ICFGNode To { get; }

		AbstractExpression GetAbstractExpresison();
	}
}

