using System;
using eBPF_verifier.Enums;
using eBPF_verifier.Interfaces;

namespace eBPF_verifier.Common
{
	public struct TristateNumber : ITristateNumberEvaluable
	{
		private Tristate[] RegisterState = new Tristate[64];

		public TristateNumber() {}

		public TristateNumber GetTristateNumber(AbstractState abstractState)
        {
			return this;
        }
    }
}

