using System;
using System.Dynamic;
using System.Text;
using eBPF_verifier.Enums;
using eBPF_verifier.Interfaces;

namespace eBPF_verifier.Common
{
    public class TristateNumber : ITristateNumberEvaluable
    {
        private Tristate[] RegisterState = new Tristate[64];

        public TristateNumber()
        {
            for (int i = 0; i < 64; i++) RegisterState[i] = Tristate.Unknown;
        }

        public TristateNumber(Tristate s)
        {
            for (int i = 0; i < 64; i++) RegisterState[i] = s;
        }

        public bool isEqualTo(TristateNumber a)
        {
            if (this == null)
            {
                return a == null ? true : false;
            }
            bool equal = true;
            for (int i = 0; i < 64; i++)
            {
                if (this.GetBitState(i) != a.GetBitState(i))
                {
                    equal = false;
                    break;
                }
            }
            return equal;
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

        public static TristateNumber GreatestLowerBound(TristateNumber a, TristateNumber b)
        {
            if (a == null) return null;
            if (b == null) return null;
            TristateNumber result = new TristateNumber();
            Tristate aBit, bBit;
            for (int i = 0; i < 64; i++)
            {
                aBit = a.GetBitState(i);
                bBit = b.GetBitState(i);
                if (aBit == Tristate.Unknown)
                {
                    result.SetBitState(i, bBit);
                }
                else if (bBit == Tristate.Unknown)
                {
                    result.SetBitState(i, aBit);
                }
                else if (aBit == bBit)
                {
                    result.SetBitState(i, aBit);
                }
                else
                {
                    result = null;
                    break;
                }

            }
            return result;
        }

        public static TristateNumber LeastUpperBound(TristateNumber a, TristateNumber b)
        {
            if (a == null) return null;
            if (b == null) return null;
            TristateNumber result = new TristateNumber();
            Tristate aBit, bBit;
            for (int i = 0; i < 64; i++)
            {
                aBit = a.GetBitState(i);
                bBit = b.GetBitState(i);
                if (aBit == Tristate.Unknown || bBit == Tristate.Unknown)
                {
                    result.SetBitState(i, Tristate.Unknown);
                }
                else if (aBit == bBit)
                {
                    result.SetBitState(i, aBit);
                }
                else
                {
                    result.SetBitState(i, Tristate.Unknown);
                }

            }
            return result;
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

