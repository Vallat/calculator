using System;
using CalculatorLibrary;

namespace ConsoleInterface
{
	class ConsoleInterface
	{
		static void Main()
		{
			Console.WriteLine("Enter 2 numbers. Available operations: +, -, *, /, ( and )");
			string? input = Console.ReadLine();
			try
			{
				Calculator calculator = new Calculator(input);
				Console.WriteLine($"Result: {calculator.do_calculation()}");
			}
			catch(Exception e)
			{
				//This is for debug
				//Console.WriteLine($"Error: {e.Message}, InnerException: {e.TargetSite}, StackTrace: {e.StackTrace}, Called method: {e.TargetSite}");
				Console.WriteLine($"Error: {e.Message}");
			}
		}
	}
}