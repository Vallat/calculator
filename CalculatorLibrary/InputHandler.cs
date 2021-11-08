namespace CalculatorLibrary;

internal class InputHandler
{
	public List<decimal> foundNumbers {get; set;}
	public List<char> foundOperators {get; set;}
	static private char[] _availableOperators {get;} = {'+', '-', '*', '/'};

	public InputHandler(string stringToProcess)
	{
		int length = stringToProcess.Length;
		if(length == 0)
		{
			throw new Exception("Input is empty");
		}
		foundNumbers = new List<decimal>();
		foundOperators = new List<char>();
		stringToProcess = SanitizeString(stringToProcess);
		ProcessString(stringToProcess);
	}

	private string SanitizeString(string string_to_sanitize)
	{
		string_to_sanitize = string_to_sanitize.Replace(".", ",");				//C# accepts only "," in decimal numbers
		string_to_sanitize = string_to_sanitize.Replace(" ", String.Empty);		//We don't need spaces
		return string_to_sanitize;
	}

	private void ProcessString(string stringToProcess)
	{
		string curDecimal = string.Empty;
		foreach(char S in stringToProcess)
		{
			bool isNumber = char.IsNumber(S) || S == ',';
			if(isNumber)
			{
				curDecimal += S;
			}

			else if(_availableOperators.Contains(S))
			{
				int length = curDecimal.Length;
				if(length > 0)
				{
					foundNumbers.Add(Convert.ToDecimal(curDecimal));
					curDecimal = string.Empty;
					foundOperators.Add(S);
				}

				else throw new Exception("Two operators in a row");
			}

			else throw new Exception("Unexpected symbols");
			
		}
		foundNumbers.Add(Convert.ToDecimal(curDecimal));
	}
}