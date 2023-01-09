using System;
using System.Text;
namespace eBPF_verifier
{
	public class AbstractExpression
	{
		private List<AbstractExpressionArgument> Arguments = new List<AbstractExpressionArgument>();

		public AbstractExpression() { }

		public void AddArgument(AbstractExpressionArgument arg)
		{
			Arguments.Add(arg);
		}

        public override string ToString()
        {
			if (Arguments.Count > 0)
			{
                var sb = new StringBuilder();
                for (var i = 0; i < (Arguments.Count - 1); i++)
                {
                    sb.Append(Arguments[i]);
                    sb.Append(" ⊔ ");
                }
                sb.Append(Arguments.Last());
                return sb.ToString();
            }
            return " ∅ ";
        }
    }
}

