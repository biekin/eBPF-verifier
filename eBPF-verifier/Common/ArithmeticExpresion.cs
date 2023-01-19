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
            var i1 = Arg1.GetInterval(abstractState);
            var i2 = Arg2.GetInterval(abstractState);
            var op = Arithmetic2IntervalOperation(Operation);
            return Interval.PerformIntervalOperation(i1, i2, op);
        }

        public static IntervalOperation Arithmetic2IntervalOperation(ArithmeticOperation arithmeticOperation)
        {
            switch (arithmeticOperation)
            {
                case ArithmeticOperation.Add:
                    return IntervalOperation.Add;
                default:
                    return IntervalOperation.Subtract;
            }
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

