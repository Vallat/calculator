namespace CalculatorLibrary;

public class Calculator
{
	private InputHandler handler { get; set; }
	
	//Init and check if InputHandler did everything fine
	public Calculator(string? input_string)
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

	//Call after initializing "Calculator" to do calculations and get the result
	public decimal do_calculation()
	{
		if (handler is null)
		{
			throw new Exception("Calculations called before initializing");
		}

		//First we find and do multiplications and divisions
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
			//We remove numbers that we won't use in the future so they don't bother us
			handler.foundNumbers.RemoveAt(iterator);
			handler.foundNumbers[iterator] = result;
			passed_operators.Add(_operator);
		}

		//We don't need multiplication and division operators anymore so lets remove them from the process queue
		foreach(char _operator in passed_operators)
		{
			handler.foundOperators.Remove(_operator);
		}

		//Now we do the rest - sums and differences
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
			//Here we remove nubmers that we calculated too, we don't need to store them
			handler.foundNumbers.RemoveAt(iterator);
			handler.foundNumbers[iterator] = result;
		}

		//Return the one element that remains - the result of our calculations
		return handler.foundNumbers[0];
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