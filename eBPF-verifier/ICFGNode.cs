using System;
namespace eBPF_verifier
{
	public interface ICFGNode
	{
		CFG Cfg { get; set; }
		Equation GetEquation();
	}
}

