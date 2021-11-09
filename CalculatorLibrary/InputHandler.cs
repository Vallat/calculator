namespace CalculatorLibrary;

internal class InputHandler
{
	public List<decimal> foundNumbers { get; set; }
	public List<char> foundOperators { get; set; }
	public List<int> operatorsPriority { get; set; }
	static private char[] _availableOperators { get; } = { '+', '-', '*', '/' };
	public int highest_operators_priority = Int32.MinValue;

	public InputHandler(string? stringToProcess)
	{
		//If string is empty give the error and exit the program
		if (stringToProcess is null || stringToProcess.Length == 0)
		{
			throw new Exception("Input is empty");
		}
		//Init arrays of numbers and operators that we gonna find
		foundNumbers = new List<decimal>();
		foundOperators = new List<char>();
		operatorsPriority = new List<int>();
		//Sanitizing is an essential part of every user input!
		stringToProcess = SanitizeString(stringToProcess);
		//Now we are ready to take numbers and operators from the string
		ProcessString(stringToProcess);
	}

	private string SanitizeString(string string_to_sanitize)
	{
		//C# accepts only "," in decimal numbers
		string_to_sanitize = string_to_sanitize.Replace(".", ",");
		//Spaces are useless, we don't need them :(
		string_to_sanitize = string_to_sanitize.Replace(" ", String.Empty);
		return string_to_sanitize;
	}

	private void ProcessString(string stringToProcess)
	{
		string curDecimal = string.Empty;
		int current_priority = 0;
		foreach (char S in stringToProcess)
		{
			//We put together individual numbers along with "," char to make our decimal number 
			bool isNumber = char.IsNumber(S) || S == ',';
			if (isNumber)
			{
				curDecimal += S;
			}

			else if (S == '(')
			{
				current_priority++;
				if (current_priority > highest_operators_priority)
					highest_operators_priority = current_priority;
			}

			else if (S == ')')
			{
				current_priority--;
				if (current_priority < 0)
					throw new Exception("Incorrect placement of brackets");
			}

			//Found something not a number or a comma? Lets check if it is an operator we can use
			else if (_availableOperators.Contains(S))
			{
				//If we don't have any characters stored in the string for decimal number than our previous character was an operator and we need to throw an error
				int length = curDecimal.Length;
				if (length > 0)
				{
					//We found an operator - add it and the decimal to the arrays they belongs to
					add_decimal(Convert.ToDecimal(curDecimal));
					curDecimal = string.Empty;
					add_operator(S);
					operatorsPriority.Add(current_priority);
				}

				else throw new Exception("Two operators in a row");
			}

			//Otherwise throw an error
			else throw new Exception("Unexpected symbols");

		}
		if (current_priority != 0)
			throw new Exception("Incorrect placement of brackets");
		//No more characters? Add the last found decimal to the array
		add_decimal(Convert.ToDecimal(curDecimal));
	}

	private void add_operator(char _operator, bool in_the_beginning = false)
	{
		if (in_the_beginning)
			foundOperators.Insert(0, _operator);
		else
			foundOperators.Add(_operator);
	}

	private void add_decimal(decimal _deicmal, bool in_the_beginning = false)
	{
		if (in_the_beginning)
			foundNumbers.Insert(0, _deicmal);
		else
			foundNumbers.Add(_deicmal);
	}

}
