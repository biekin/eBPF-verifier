using System;
using eBPF_verifier.Common;

namespace eBPF_verifier.Interfaces
{
	public interface ITristateNumberEvaluable
	{
		TristateNumber GetTristateNumber(AbstractState abstractState);
	}
}

