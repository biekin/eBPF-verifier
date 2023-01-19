using System;
using System.Text;
namespace eBPF_verifier
{
	public class Analyzer
	{
		private CFG Cfg;

		private IIterator Iterator;

		public List<Equation> Equations { get; private set; }

		public Analyzer(CFG cfg, IIterator iterator)
		{
            Equations = new List<Equation>();
            Cfg = cfg;
			Iterator = iterator;
		}

		public void GenerateEquations()
		{
			foreach(var n in Cfg.Nodes)
			{
				Equations.Add(GetEquation(n));
			}
		}

		private Equation GetEquation(ProgramPoint n)
		{
			var nodeEquation = n.GetEquation();
			return nodeEquation;
		}

		public Solution Solve()
		{
			return Iterator.Solve(this);
		}

		public void PrintEquations()
		{
			var sb = new StringBuilder();
			sb.Append($"{Equations.Count} Equation(s):\n");

			foreach(var eq in Equations)
			{
				sb.Append(eq.ToString());
			}

			Console.WriteLine(sb.ToString());
		}

		public Solution GetCurrentState()
		{
			var currentSolutionCandidate = new Solution();
			foreach(var eq in Equations)
			{
				currentSolutionCandidate.AddOrUpdateProgramPoint(eq.ProgramPoint.Label, eq.ProgramPoint.AbstractState);
			}
			return currentSolutionCandidate;
		}

		public void PrintCurrentStates()
		{
			var sb = new StringBuilder();
			foreach(var pp in Cfg.Nodes)
			{
				sb.Append($"{pp}:\n{pp.AbstractState}\n");
			}
			Console.WriteLine(sb.ToString());
        }
	}
}

