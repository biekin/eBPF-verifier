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
                case ArithmeticOperation.Subtract:
                    return IntervalOperation.Subtract;
                case ArithmeticOperation.Multiply:
                    return IntervalOperation.Multiply;
                case ArithmeticOperation.Divide:
                    return IntervalOperation.Divide;
                case ArithmeticOperation.Modulo:
                    return IntervalOperation.Modulo;
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
                    case ArithmeticOperation.Subtract:
                        return "-";
                    case ArithmeticOperation.Multiply:
                        return "*";
                    case ArithmeticOperation.Divide:
                        return "/";
                    case ArithmeticOperation.Modulo:
                        return "%";
                    default:
                        return "-";
                }
            }
            return $"{Arg1} {GetArithmeticOperationString(Operation)} {Arg2}";
        }
    }
}

