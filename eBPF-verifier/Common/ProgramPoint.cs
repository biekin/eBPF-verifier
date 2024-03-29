﻿using System;
namespace eBPF_verifier
{
	public class ProgramPoint
	{

		public CFG Cfg { get; set; }
		public string Label { get; private set; }
		public AbstractState AbstractState = new AbstractState();

		public ProgramPoint(string label)
		{
			Label = label;
		}

		public Equation GetEquation()
		{
			AbstractState = Cfg.GetBlankAbstractState();
			AbstractExpression abstractExpression = new AbstractExpression();
			var edgesToThis = Cfg.Edges.Where(e => e.To == this);

			foreach (var edge in edgesToThis)
			{
				var programPointFrom = edge.From;
				var edgeExpression = edge.GetEdgeExpression();
				abstractExpression.AddArgument(new AbstractExpressionArgument(programPointFrom, edgeExpression));
			}

			return new Equation(this, abstractExpression);
		}

        public override string ToString()
        {
			return $"PP#{Label}";
        }
    }
}

