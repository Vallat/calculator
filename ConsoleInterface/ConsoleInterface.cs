using System;
using CalculatorLibrary;

namespace ConsoleInterface
{
	class ConsoleInterface
	{
		static void Main()
		{
			Calculator calculator = new Calculator();
			Console.WriteLine("Enter 2 numbers. Available operations: +, -, *, /");
			string? input = Console.ReadLine();
			if(input is null)
				return;
			input = input.Replace(".", ",");
			string[] strings = input.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

			decimal result = strings[1][0] switch 
			{
				'+' => calculator.do_sum(Convert.ToDecimal(strings[0]), Convert.ToDecimal(strings[2])),
				'-' => calculator.do_sum(Convert.ToDecimal(strings[0]), -Convert.ToDecimal(strings[2])),
				'*' => calculator.do_multiplication(Convert.ToDecimal(strings[0]), Convert.ToDecimal(strings[2])),
				'/' => calculator.do_division(Convert.ToDecimal(strings[0]), Convert.ToDecimal(strings[2])),
				_ => 0
			};
			Console.WriteLine($"{strings[0]} {strings[1]} {strings[2]} = {result}");
		}
	}
}