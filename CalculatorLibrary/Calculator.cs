namespace CalculatorLibrary;

public class Calculator
{
	private InputHandler handler { get; set; }
	
	public Calculator(string input_string)
	{
		handler = new InputHandler(input_string);

		if (handler.foundOperators is null || handler.foundNumbers is null)
		{
			throw new Exception("Not enough numbers or operators!");
		}

		if (handler.foundNumbers.Count - 1 != handler.foundOperators.Count)
		{
			throw new Exception("Somehow there are more or not enough operators");
		}

	}

	public decimal do_calculation()
	{
		if (handler is null)
		{
			throw new Exception("Calculations called before initializing");
		}

		int iterator = 0;
		List<char> passed_operators = new List<char>();
		foreach (char _operator in handler.foundOperators)
		{
			decimal result = 0;
			if(_operator == '*')
				result = do_multiplication(handler.foundNumbers[iterator], handler.foundNumbers[iterator + 1]);
			else if(_operator == '/') 
				result = do_division(handler.foundNumbers[iterator], handler.foundNumbers[iterator + 1]);
			else
			{
				iterator++;
				continue;
			}
			handler.foundNumbers.RemoveAt(iterator);
			handler.foundNumbers[iterator] = result;
			passed_operators.Add(_operator);
		}

		foreach(char _operator in passed_operators)
		{
			handler.foundOperators.Remove(_operator);
		}

		iterator = 0;
		foreach (char _operator in handler.foundOperators)
		{
			decimal result = 0;
			if(_operator == '+')
				result = do_sum(handler.foundNumbers[iterator], handler.foundNumbers[iterator + 1]);
			else if(_operator == '-')
				result = do_sum(handler.foundNumbers[iterator], handler.foundNumbers[iterator + 1]);
			else
			{
				iterator++;
				continue;
			}
			iterator++;
			handler.foundNumbers[iterator] = result;
		}

		return handler.foundNumbers[handler.foundNumbers.Count - 1];
	}

	public decimal do_sum(decimal first_num, decimal second_num)
	{
		return first_num + second_num;
	}

	public decimal do_multiplication(decimal first_num, decimal second_num)
	{
		return first_num * second_num;
	}

	public decimal do_division(decimal first_num, decimal second_num)
	{
		return first_num / second_num;
	}
}