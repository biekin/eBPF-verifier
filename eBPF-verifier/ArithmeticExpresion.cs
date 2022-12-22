using System;
namespace eBPF_verifier
{
	public class ArithmeticExpresion : IExpression
	{
        private IArgument Arg1;

        private IArgument Arg2;

        private string Operation;

        public ArithmeticExpresion(IArgument arg1, IArgument arg2, string operation)
		{
            Arg1 = arg1;
            Arg2 = arg2;
            Operation = operation;
		}
    }
}

