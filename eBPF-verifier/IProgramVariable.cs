using System;
namespace eBPF_verifier
{
	public interface IProgramVariable : IArgument
	{
		string Name { get; }
	}
}

