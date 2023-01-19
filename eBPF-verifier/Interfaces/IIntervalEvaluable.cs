using System;
namespace eBPF_verifier
{
	public interface IIntervalEvaluable
	{
		Interval GetInterval(AbstractState abstractState);
	}
}

