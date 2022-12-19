using System;
namespace eBPF_verifier
{
	public interface ICFGEdge
	{
		ICFGNode from { get; }
		ICFGNode To { get; }

		AbstractExpression GetAbstractExpresison();
	}
}

