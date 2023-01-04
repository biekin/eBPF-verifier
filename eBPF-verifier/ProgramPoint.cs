using System;
namespace eBPF_verifier
{
	public class ProgramPoint : ICFGNode
	{

		public CFG Cfg { get; set; }
		public string Label { get; private set; }

		public ProgramPoint(string label)
		{
			Label = label;
		}

		public Equation GetEquation()
		{
			AbstractExpression GetAbstracExpressionFromListOfEdges(List<ICFGEdge> edges)
			{
				if(edges.Count == 2)
				{
					return new AbstractExpression(edges[0].GetAbstractExpresison(), edges[1].GetAbstractExpresison());
				}
				else
				{
					return new AbstractExpression(edges[0].GetAbstractExpresison(), GetAbstracExpressionFromListOfEdges(edges.Skip(1).ToList()));
				}
			}

			var edgesToThis = Cfg.Edges.Where(e => e.To == this).ToList();
			//	var abstractExpression = new AbstractExpression(); // TODO
			//	var abstractState = new AbstractState(); // TODO
			//	return new Equation(this, abstractExpression, abstractState);

			throw new NotImplementedException();
		}

        public override string ToString()
        {
			return Label;
        }
    }
}

