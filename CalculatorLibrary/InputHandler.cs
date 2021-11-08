namespace CalculatorLibrary;

internal class InputHandler
{
	public List<decimal> foundNumbers {get; set;}
	public List<char> foundOperators {get; set;}
	static private char[] _availableOperators {get;} = {'+', '-', '*', '/'};

	public InputHandler(string? stringToProcess)
	{
		//If string is empty give the error and exit the program
		if(stringToProcess is null || stringToProcess.Length == 0)
		{
			throw new Exception("Input is empty");
		}
		//Init arrays of numbers and operators that we gonna find
		foundNumbers = new List<decimal>();
		foundOperators = new List<char>();
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
		foreach(char S in stringToProcess)
		{
			//We put together individual numbers along with "," char to make our decimal number 
			bool isNumber = char.IsNumber(S) || S == ',';
			if(isNumber)
			{
				curDecimal += S;
			}

			//Found something not a number or a comma? Lets check if it is an operator we can use
			else if(_availableOperators.Contains(S))
			{
				//If we don't have any characters stored in the string for decimal number than our previous character was an operator and we need to throw an error
				int length = curDecimal.Length;
				if(length > 0)
				{
					//We found an operator - add it and the decimal to the arrays they belongs to
					foundNumbers.Add(Convert.ToDecimal(curDecimal));
					curDecimal = string.Empty;
					foundOperators.Add(S);
				}

				else throw new Exception("Two operators in a row");
			}

			//Otherwise throw an error
			else throw new Exception("Unexpected symbols");
			
		}
		//No more characters? Add the last found decimal to the array
		foundNumbers.Add(Convert.ToDecimal(curDecimal));
	}
}