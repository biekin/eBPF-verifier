using System;
using eBPF_verifier.Enums;

namespace eBPF_verifier.Common
{
	public static class TristateCalculator
	{
        public static TristateNumber Add(TristateNumber a, TristateNumber b)
        {
            TristateNumber result = new TristateNumber();
            Tristate carry = Tristate.Zero;

            for (int i = 63; i >= 0; i--)
            {
                var bitA = a.GetBitState(i);
                var bitB = b.GetBitState(i);
                if (bitA == Tristate.Unknown || bitB == Tristate.Unknown) break;
                else
                {
                    switch ((bitA, bitB, carry))
                    {
                        case (Tristate.Zero, Tristate.Zero, Tristate.Zero):
                            result.SetBitState(i, Tristate.Zero);
                            carry = Tristate.Zero;
                            break;
                        case (Tristate.Zero, Tristate.Zero, Tristate.One):
                            result.SetBitState(i, Tristate.One);
                            carry = Tristate.Zero;
                            break;
                        case (Tristate.Zero, Tristate.Zero, Tristate.Unknown):
                            result.SetBitState(i, Tristate.Unknown);
                            carry = Tristate.Zero;
                            break;
                        case (Tristate.Zero, Tristate.One, Tristate.Zero):
                            result.SetBitState(i, Tristate.One);
                            carry = Tristate.Zero;
                            break;
                        case (Tristate.Zero, Tristate.One, Tristate.One):
                            result.SetBitState(i, Tristate.Zero);
                            carry = Tristate.One;
                            break;
                        case (Tristate.Zero, Tristate.One, Tristate.Unknown):
                            result.SetBitState(i, Tristate.Unknown);
                            carry = Tristate.Unknown;
                            break;
                        case (Tristate.Zero, Tristate.Unknown, Tristate.Zero):
                            result.SetBitState(i, Tristate.Unknown);
                            carry = Tristate.Zero;
                            break;
                        case (Tristate.Zero, Tristate.Unknown, Tristate.One):
                            result.SetBitState(i, Tristate.Unknown);
                            carry = Tristate.Unknown;
                            break;
                        case (Tristate.Zero, Tristate.Unknown, Tristate.Unknown):
                            result.SetBitState(i, Tristate.Unknown);
                            carry = Tristate.Unknown;
                            break;
                        case (Tristate.One, Tristate.Zero, Tristate.Zero):
                            result.SetBitState(i, Tristate.One);
                            carry = Tristate.Zero;
                            break;
                        case (Tristate.One, Tristate.Zero, Tristate.One):
                            result.SetBitState(i, Tristate.Zero);
                            carry = Tristate.One;
                            break;
                        case (Tristate.One, Tristate.Zero, Tristate.Unknown):
                            result.SetBitState(i, Tristate.Unknown);
                            carry = Tristate.Unknown;
                            break;
                        case (Tristate.One, Tristate.One, Tristate.Zero):
                            result.SetBitState(i, Tristate.Zero);
                            carry = Tristate.One;
                            break;
                        case (Tristate.One, Tristate.One, Tristate.One):
                            result.SetBitState(i, Tristate.One);
                            carry = Tristate.One;
                            break;
                        case (Tristate.One, Tristate.One, Tristate.Unknown):
                            result.SetBitState(i, Tristate.Unknown);
                            carry = Tristate.One;
                            break;
                        case (Tristate.One, Tristate.Unknown, Tristate.Zero):
                            result.SetBitState(i, Tristate.Unknown);
                            carry = Tristate.Unknown;
                            break;
                        case (Tristate.One, Tristate.Unknown, Tristate.One):
                            result.SetBitState(i, Tristate.One);
                            carry = Tristate.Unknown;
                            break;
                        case (Tristate.One, Tristate.Unknown, Tristate.Unknown):
                            result.SetBitState(i, Tristate.Unknown);
                            carry = Tristate.Unknown;
                            break;
                        case (Tristate.Unknown, Tristate.Zero, Tristate.Zero):
                            result.SetBitState(i, Tristate.Unknown);
                            carry = Tristate.Zero;
                            break;
                        case (Tristate.Unknown, Tristate.Zero, Tristate.One):
                            result.SetBitState(i, Tristate.Unknown);
                            carry = Tristate.Unknown;
                            break;
                        case (Tristate.Unknown, Tristate.Zero, Tristate.Unknown):
                            result.SetBitState(i, Tristate.Unknown);
                            carry = Tristate.Unknown;
                            break;
                        case (Tristate.Unknown, Tristate.One, Tristate.Zero):
                            result.SetBitState(i, Tristate.Unknown);
                            carry = Tristate.Unknown;
                            break;
                        case (Tristate.Unknown, Tristate.One, Tristate.One):
                            result.SetBitState(i, Tristate.Unknown);
                            carry = Tristate.One;
                            break;
                        case (Tristate.Unknown, Tristate.One, Tristate.Unknown):
                            result.SetBitState(i, Tristate.Unknown);
                            carry = Tristate.Unknown;
                            break;
                        case (Tristate.Unknown, Tristate.Unknown, Tristate.Zero):
                            result.SetBitState(i, Tristate.Unknown);
                            carry = Tristate.Unknown;
                            break;
                        case (Tristate.Unknown, Tristate.Unknown, Tristate.One):
                            result.SetBitState(i, Tristate.Unknown);
                            carry = Tristate.Unknown;
                            break;
                        case (Tristate.Unknown, Tristate.Unknown, Tristate.Unknown):
                            result.SetBitState(i, Tristate.Unknown);
                            carry = Tristate.Unknown;
                            break;
                    }
                }
            }

            return result;
        }

        public static TristateNumber Subtract(TristateNumber a, TristateNumber b)
        {
            TristateNumber result = new TristateNumber();
            Tristate borrow = Tristate.Zero;

            for (int i = 63; i >= 0; i--)
            {
                var bitA = a.GetBitState(i);
                var bitB = b.GetBitState(i);
                if (bitA == Tristate.Unknown || bitB == Tristate.Unknown) break;
                else
                {
                    switch ((bitA, bitB, borrow))
                    {
                        case (Tristate.Zero, Tristate.Zero, Tristate.Zero):
                            result.SetBitState(i, Tristate.Zero);
                            borrow = Tristate.Zero;
                            break;
                        case (Tristate.Zero, Tristate.Zero, Tristate.One):
                            result.SetBitState(i, Tristate.One);
                            borrow = Tristate.One;
                            break;
                        case (Tristate.Zero, Tristate.Zero, Tristate.Unknown):
                            result.SetBitState(i, Tristate.Unknown);
                            borrow = Tristate.Unknown;
                            break;
                        case (Tristate.Zero, Tristate.One, Tristate.Zero):
                            result.SetBitState(i, Tristate.One);
                            borrow = Tristate.One;
                            break;
                        case (Tristate.Zero, Tristate.One, Tristate.One):
                            result.SetBitState(i, Tristate.Zero);
                            borrow = Tristate.One;
                            break;
                        case (Tristate.Zero, Tristate.One, Tristate.Unknown):
                            result.SetBitState(i, Tristate.Unknown);
                            borrow = Tristate.One;
                            break;
                        case (Tristate.Zero, Tristate.Unknown, Tristate.Zero):
                            result.SetBitState(i, Tristate.Unknown);
                            borrow = Tristate.Unknown;
                            break;
                        case (Tristate.Zero, Tristate.Unknown, Tristate.One):
                            result.SetBitState(i, Tristate.Unknown);
                            borrow = Tristate.One;
                            break;
                        case (Tristate.Zero, Tristate.Unknown, Tristate.Unknown):
                            result.SetBitState(i, Tristate.Unknown);
                            borrow = Tristate.Unknown;
                            break;
                        case (Tristate.One, Tristate.Zero, Tristate.Zero):
                            result.SetBitState(i, Tristate.One);
                            borrow = Tristate.Zero;
                            break;
                        case (Tristate.One, Tristate.Zero, Tristate.One):
                            result.SetBitState(i, Tristate.Zero);
                            borrow = Tristate.Zero;
                            break;
                        case (Tristate.One, Tristate.Zero, Tristate.Unknown):
                            result.SetBitState(i, Tristate.Unknown);
                            borrow = Tristate.Zero;
                            break;
                        case (Tristate.One, Tristate.One, Tristate.Zero):
                            result.SetBitState(i, Tristate.Zero);
                            borrow = Tristate.Zero;
                            break;
                        case (Tristate.One, Tristate.One, Tristate.One):
                            result.SetBitState(i, Tristate.One);
                            borrow = Tristate.One;
                            break;
                        case (Tristate.One, Tristate.One, Tristate.Unknown):
                            result.SetBitState(i, Tristate.Unknown);
                            borrow = Tristate.Unknown;
                            break;
                        case (Tristate.One, Tristate.Unknown, Tristate.Zero):
                            result.SetBitState(i, Tristate.Unknown);
                            borrow = Tristate.Zero;
                            break;
                        case (Tristate.One, Tristate.Unknown, Tristate.One):
                            result.SetBitState(i, Tristate.Unknown);
                            borrow = Tristate.Unknown;
                            break;
                        case (Tristate.One, Tristate.Unknown, Tristate.Unknown):
                            result.SetBitState(i, Tristate.Unknown);
                            borrow = Tristate.Unknown;
                            break;
                        case (Tristate.Unknown, Tristate.Zero, Tristate.Zero):
                            result.SetBitState(i, Tristate.Unknown);
                            borrow = Tristate.Zero;
                            break;
                        case (Tristate.Unknown, Tristate.Zero, Tristate.One):
                            result.SetBitState(i, Tristate.Unknown);
                            borrow = Tristate.Unknown;
                            break;
                        case (Tristate.Unknown, Tristate.Zero, Tristate.Unknown):
                            result.SetBitState(i, Tristate.Unknown);
                            borrow = Tristate.Unknown;
                            break;
                        case (Tristate.Unknown, Tristate.One, Tristate.Zero):
                            result.SetBitState(i, Tristate.Unknown);
                            borrow = Tristate.Unknown;
                            break;
                        case (Tristate.Unknown, Tristate.One, Tristate.One):
                            result.SetBitState(i, Tristate.Unknown);
                            borrow = Tristate.One;
                            break;
                        case (Tristate.Unknown, Tristate.One, Tristate.Unknown):
                            result.SetBitState(i, Tristate.Unknown);
                            borrow = Tristate.Unknown;
                            break;
                        case (Tristate.Unknown, Tristate.Unknown, Tristate.Zero):
                            result.SetBitState(i, Tristate.Unknown);
                            borrow = Tristate.Unknown;
                            break;
                        case (Tristate.Unknown, Tristate.Unknown, Tristate.One):
                            result.SetBitState(i, Tristate.Unknown);
                            borrow = Tristate.Unknown;
                            break;
                        case (Tristate.Unknown, Tristate.Unknown, Tristate.Unknown):
                            result.SetBitState(i, Tristate.Unknown);
                            borrow = Tristate.Unknown;
                            break;
                    }
                }
            }

            return result;

        }

        public static TristateNumber Multiply(TristateNumber a, TristateNumber b)
        {
            TristateNumber result = new TristateNumber(Tristate.Zero);

            for (int i = 63; i >= 0; i--)
            {
                TristateNumber partialResult = new TristateNumber(Tristate.Zero);

                    for (int j = 63; j >= 0; j--)
                    {
                        var index = j - (63 - i);
                        if (index < 0) break;
                        switch (a.GetBitState(j), b.GetBitState(i))
                        {
                            case (Tristate.Zero, _):
                                partialResult.SetBitState(index, Tristate.Zero);
                                break;
                            case (_, Tristate.Zero):
                                partialResult.SetBitState(index, Tristate.Zero);
                                break;
                            case (Tristate.One, Tristate.One):
                                partialResult.SetBitState(index, Tristate.One);
                                break;
                            default:
                                partialResult.SetBitState(index, Tristate.Unknown);
                                break;
                        }
                    }

                    result = Add(result, partialResult);
            }

            return result;
        }


        public static TristateNumber Divide(TristateNumber a, TristateNumber b)
        {
            TristateNumber result = new TristateNumber(Tristate.Zero);
            TristateNumber remainder = new TristateNumber(Tristate.Zero);

            for (int i = 63; i >= 0; i--)
            {
                remainder = ShiftLeft(remainder, 1);
                remainder.SetBitState(0, a.GetBitState(i));

                if (IsGreaterOrEqual(remainder, b))
                {
                    remainder = Subtract(remainder, b);
                    result.SetBitState(i, Tristate.One);
                }
            }

            return result;
        }

        public static TristateNumber Modulo(TristateNumber a, TristateNumber b)
        {
            TristateNumber remainder = new TristateNumber(Tristate.Zero);

            for (int i = 63; i >= 0; i--)
            {
                remainder = ShiftLeft(remainder, 1);
                remainder.SetBitState(0, a.GetBitState(i));

                if (IsGreaterOrEqual(remainder, b))
                {
                    remainder = Subtract(remainder, b);
                }
            }

            return remainder;
        }

        private static TristateNumber ShiftLeft(TristateNumber number, int shiftAmount)
        {
            TristateNumber shiftedNumber = new TristateNumber();

            for (int i = 0; i < 64 - shiftAmount; i++)
            {
                shiftedNumber.SetBitState(i + shiftAmount, number.GetBitState(i));
            }

            return shiftedNumber;
        }

        private static bool IsGreaterOrEqual(TristateNumber a, TristateNumber b)
        {
            for (int i = 63; i >= 0; i--)
            {
                var bitA = a.GetBitState(i);
                var bitB = b.GetBitState(i);

                if (bitA == Tristate.Unknown || bitB == Tristate.Unknown)
                {
                    // If any bit is unknown, it cannot be determined if a is greater or equal to b
                    return false;
                }

                if (bitA == Tristate.One && bitB == Tristate.Zero)
                {
                    // If a has a one bit and b has a zero bit, a is greater
                    return true;
                }

                if (bitA == Tristate.Zero && bitB == Tristate.One)
                {
                    // If a has a zero bit and b has a one bit, a is not greater
                    return false;
                }
            }

            // If all bits are equal, a is equal to b or greater
            return true;
        }
    }
}

