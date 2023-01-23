using System;
namespace eBPF_verifier
{
	public class Example6
	{
        // EXAMPLE 5
        //    x := 100
        //    do {
        //        x := x-1
        //    } while x > 0

        public Example6() { }

		public void Execute()
		{
            Console.WriteLine(Directory.GetCurrentDirectory());
            List<EBPFInstruction> inst = ParserBin.parse("./eBPF-verifier/Examples/files/Example1");
            inst.ForEach(i => Console.WriteLine(i.instruction + " " + i.dst + " " + i.src + " " + i.immediate + " " + i.offset));
            CFG cfg = CFGBuilder.build(inst);
            // Console.WriteLine(cfg.ToString());
            var analyzer = new Analyzer(cfg, new BasicIterator(102));
            analyzer.GenerateEquations();
            analyzer.PrintEquations();
            analyzer.PrintCurrentStates();
            var solution = analyzer.Solve();
            Console.WriteLine(solution);
        }
    }
}

