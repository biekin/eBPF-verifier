using System;
namespace eBPF_verifier
{
	public class Example5
	{
        // EXAMPLE 5
        //    x := 100
        //    do {
        //        x := x-1
        //    } while x > 1

        public Example5() { }

		public void Execute()
		{
            List<EBPFInstruction> inst = ParserAsm.parse("./Examples/files/Example1A");
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

