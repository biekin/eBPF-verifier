using System;
using System.Dynamic;
using System.Text;
using eBPF_verifier.Enums;
using eBPF_verifier.Interfaces;

namespace eBPF_verifier.Common
{
    public struct TristateNumber : ITristateNumberEvaluable
    {
        private Tristate[] RegisterState = new Tristate[64];

        public TristateNumber() { }

        public TristateNumber(Tristate s)
        {
            for (int i = 0; i < 64; i++) RegisterState[i] = s;
        }

        public Tristate GetBitState(int n)
        {
            if (n > 63 || n < 0) throw new ArgumentException("Tristate numbers have 64 bits. Provide bit index between 0 and 63.");
            else return RegisterState[n];
        }

        public void SetBitState(int n, Tristate s)
        {
            if (n > 63 || n < 0) throw new ArgumentException("Tristate numbers have 64 bits. Provide bit index between 0 and 63.");
            else RegisterState[n] = s;
        }

        public TristateNumber GetTristateNumber(AbstractState abstractState)
        {
			return this;
        }

        public override string ToString()
        {
            string TristateToString(Tristate s)
            {
                switch (s)
                {
                    case Tristate.Unknown:
                        return "? ";
                    case Tristate.Zero:
                        return "0 ";
                    case Tristate.One:
                        return "1 ";
                    default:
                        return "";
                }
            }

            var sb = new StringBuilder();
            var first = RegisterState[0];
            sb.Append(TristateToString(first));
            int i = 1;
            while (RegisterState[i] == first) i++;
            if (i > 1) sb.Append($".. {TristateToString(first)}");
            while(i < 64)
            {
                sb.Append(TristateToString(RegisterState[i]));
                i++;
            }
            return sb.ToString();
        }
    }
}

