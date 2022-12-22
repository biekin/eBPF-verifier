using System;
namespace eBPF_verifier
{
	public class Analyzer
	{
		private CFG Cfg;

		private IIterator Iterator;

		public Analyzer(CFG cfg, IIterator iterator)
		{
			Cfg = cfg;
			Iterator = iterator;
		}
	}
}

