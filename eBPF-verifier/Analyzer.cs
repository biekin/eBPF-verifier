using System;
namespace eBPF_verifier
{
	public class Analyzer
	{
		private CFG Cfg;

		private IIterator Iterator;

		private List<Equation> Equations = new List<Equation>;

		public Analyzer(CFG cfg, IIterator iterator)
		{
			Cfg = cfg;
			Iterator = iterator;
		}

		private void GenerateEquations()
		{
			foreach(var n in Cfg.Nodes)
			{
				Equations.Add(GetEquation(n));
			}
		}

		private Equation GetEquation(ICFGNode n)
		{
			var nodeEquation = n.GetEquation();
			return nodeEquation;
		}
	}
}

