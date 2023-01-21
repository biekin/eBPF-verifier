using System;
using System.ComponentModel;

namespace eBPF_verifier
{
	public enum ArithmeticOperation
	{
        [Description("+")]
        Add,
        [Description("-")]
        Subtract,
        [Description("*")]
        Multiply,
        [Description("/")]
        Divide,
        [Description("%")]
        Modulo
	}
}

