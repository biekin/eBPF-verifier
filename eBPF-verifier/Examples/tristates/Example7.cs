﻿using System;
using eBPF_verifier.Common;
using eBPF_verifier.Enums;

namespace eBPF_verifier.Examples.tristates
{
	public class Example7
	{
		public Example7(){}

		public void Execute()
		{
			var n1 = new TristateNumber();
			n1.SetBitState(63, Tristate.One);
			n1.SetBitState(62, Tristate.One);
			n1.SetBitState(61, Tristate.One);

			var n2 = new TristateNumber();
            n2.SetBitState(63, Tristate.Zero);
            n2.SetBitState(62, Tristate.One);
            n2.SetBitState(61, Tristate.One);

			var n3 = TristateCalculator.Add(n1, n2);
			Console.WriteLine($"n1 = {n1} \n n2 = {n2} \n n3 = {n3}");
        }
	}
}

