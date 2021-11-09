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
			throw new Exception("Calculations were called before calculator was initialized");
		}

		handle_numbers_power();

		while (handler.operatorsPriority.Count > 0)
		{
			//First we find and do multiplications and divisions
			int iterator = 0;
			List<int> passed_operators_indexes = new List<int>();
			foreach (char _operator in handler.foundOperators)
			{
				if(handler.operatorsPriority[iterator] != handler.highest_operators_priority)
				{
					iterator++;
					continue;
				}
				decimal result = 0;
				if (_operator == '*')
					result = do_multiplication(handler.foundNumbers[iterator], handler.foundNumbers[iterator + 1]);
				else if (_operator == '/')
					result = do_division(handler.foundNumbers[iterator], handler.foundNumbers[iterator + 1]);
				else
				{
					iterator++;
					continue;
				}
				//We remove numbers that we won't use in the future so they don't bother us
				handler.foundNumbers.RemoveAt(iterator);
				handler.foundNumbers[iterator] = result;
				handler.operatorsPriority.RemoveAt(iterator);
				if(handler.operatorsPriority.Count == 0)
					break;
				passed_operators_indexes.Add(iterator);
			}

			if(handler.operatorsPriority.Count == 0)
				break;

			void remove_opartors_indexes()
			{
				if(passed_operators_indexes.Count > 0)
				{
					foreach(int index in passed_operators_indexes)
					{
						handler.foundOperators.RemoveAt(index);
					}
				}
			}
			remove_opartors_indexes();
				
			//Now we do the rest - sums and differences
			iterator = 0;
			passed_operators_indexes.Clear();
			foreach (char _operator in handler.foundOperators)
			{
				if(handler.operatorsPriority[iterator] != handler.highest_operators_priority)
				{
					iterator++;
					continue;
				}
				decimal result = 0;
				if (_operator == '+')
					result = do_sum(handler.foundNumbers[iterator], handler.foundNumbers[iterator + 1]);
				else if (_operator == '-')
					result = do_sum(handler.foundNumbers[iterator], -handler.foundNumbers[iterator + 1]);
				else
				{
					iterator++;
					continue;
				}
				//Here we remove nubmers that we calculated too, we don't need to store them
				handler.foundNumbers.RemoveAt(iterator);
				handler.foundNumbers[iterator] = result;
				handler.operatorsPriority.RemoveAt(iterator);
				if(handler.operatorsPriority.Count == 0)
					break;
				passed_operators_indexes.Add(iterator);
			}
			remove_opartors_indexes();

			handler.highest_operators_priority--;
		}

		//Return the one element that remains - the result of our calculations
		return handler.foundNumbers[0];
	}

	private void handle_numbers_power()
	{
		int curr_index = handler.foundOperators.IndexOf('^');
		if(curr_index == -1)
			return;

		do
		{
			handler.foundNumbers[curr_index] = do_pow(handler.foundNumbers[curr_index], handler.foundNumbers[curr_index+1]);
			handler.foundNumbers.RemoveAt(curr_index+1);
			handler.foundOperators.RemoveAt(curr_index);
			handler.operatorsPriority.RemoveAt(curr_index);
			curr_index = handler.foundOperators.IndexOf('^');
		}
		while(curr_index != -1);
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

	public decimal do_pow(decimal number, decimal power)
	{
		int iPower = (int)power;
		decimal remainder = power - iPower;
		decimal new_result = number;
		for(int itr = 1; itr < iPower; itr++)
		{
			new_result = do_multiplication(new_result, number);
		}
		if(remainder != 0)
			new_result *= (decimal)Math.Pow((double)number, (double)(1/remainder));
		return new_result;
	}

}