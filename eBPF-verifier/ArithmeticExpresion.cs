using System;
namespace eBPF_verifier
{
	public class ArithmeticExpresion : IProgramExpression
    {
        private IArgument Arg1;
        private IArgument Arg2;
        private ArithmeticOperation Operation;

        public ArithmeticExpresion(IArgument arg1, IArgument arg2, ArithmeticOperation operation)
		{
            Arg1 = arg1;
            Arg2 = arg2;
            Operation = operation;
		}

        public override string ToString()
        {
            return $"{Arg1} {Operation} {Arg2}";
        }
    }
}

