using System;
namespace eBPF_verifier
{
	public interface IArgument
	{
		Interval GetInterval();
	}
}

