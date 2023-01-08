using System;
namespace eBPF_verifier
{
	public interface ICFGEdge
	{
        ProgramPoint From { get; }
        ProgramPoint To { get; }

		IEdgeExpression GetEdgeExpresison();
	}
}

