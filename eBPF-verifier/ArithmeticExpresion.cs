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

        public Interval GetInterval(AbstractState abstractState)
        {
            throw new NotImplementedException(); // TODO 
        }

        public override string ToString()
        {
            string GetArithmeticOperationString(ArithmeticOperation op)
            {
                switch (op)
                {
                    case ArithmeticOperation.Add:
                        return "+";
                    default:
                        return "-";
                }
            }
            return $"{Arg1} {GetArithmeticOperationString(Operation)} {Arg2}";
        }
    }
}

