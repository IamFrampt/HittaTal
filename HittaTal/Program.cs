string userInput = string.Empty;
ulong totalSum;
List<string> allFoundMatches = new List<string>();
bool wasStringIncorrectAtLeastOnce = false;
bool isSumToHigh = false;
Console.ForegroundColor = ConsoleColor.White;

//------------------Main------------------

do
{
    Console.Write("User input: ");
    userInput = Console.ReadLine();
    Console.WriteLine();
    //%%hej1234554321hej%%123321

    allFoundMatches = CheckingForMatchingNumbers(userInput);
    totalSum = AddsAllFoundMatches(allFoundMatches);
    PrintsResult(userInput, allFoundMatches, totalSum);

    if (allFoundMatches.Count == 0)
    {
        Console.Clear();
        Console.WriteLine("String was invalid. Try a new one.");
        Console.WriteLine();
    }

}
while (allFoundMatches.Count == 0);

//------------------Metoder------------------

List<string> CheckingForMatchingNumbers(string userInput)
{
    List<string> allFoundMatches = new List<string>();

    for (int currentNumber = 0; currentNumber < userInput.Length; currentNumber++)
    {
        for (int secondNumber = currentNumber + 1; secondNumber < userInput.Length; secondNumber++)
        {
            if (char.IsNumber(userInput[currentNumber]) && char.IsNumber(userInput[secondNumber]))
            {
                if (IsStringValid(userInput, currentNumber, secondNumber))
                {
                    allFoundMatches.Add(userInput.Substring(currentNumber, secondNumber - currentNumber + 1));
                    break;
                }
            }
            else
                break;
        }
    }

    return allFoundMatches;
}

bool IsStringValid(string userInput, int currentNumber, int secondNumber)
{
    if (char.IsDigit(userInput[currentNumber]) && userInput.Substring(secondNumber, userInput.Length - secondNumber).Contains(userInput[currentNumber]))
    {
        if (!userInput.Substring(currentNumber, secondNumber - currentNumber).Any(c => char.IsLetter(c)))
        {
            if (userInput[currentNumber] == userInput[secondNumber])
            {
                return true;
            }
        }
    }
    return false;

}

ulong AddsAllFoundMatches(List<string> allFoundMatches)
{
    try
    {
        ulong totalSum = 0;

        for (int i = 0; i < allFoundMatches.Count; i++)
        {
            totalSum += Convert.ToUInt64(allFoundMatches[i]);
        }

        return totalSum;
    }
    catch
    {
        isSumToHigh = true;
        return 0;
    }
}

void PrintsResult(string userInput, List<string> allFoundMatches, ulong totalSum)
{
    int stringRow = 1;
    int HoldsStringPosition = 0;

    foreach (string match in allFoundMatches)
    {
        for (int currentPositionInString = HoldsStringPosition; currentPositionInString < userInput.Length; currentPositionInString++)
        {
            if (userInput.Substring(currentPositionInString, match.Length).Contains(match))
            {
                Console.Write($"{stringRow.ToString().PadRight(3)}: {userInput.Substring(0, currentPositionInString)}");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write(userInput.Substring(currentPositionInString, match.Length));
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(userInput.Substring(currentPositionInString + match.Length, userInput.Length - (currentPositionInString + match.Length)));

                stringRow++;
                HoldsStringPosition = currentPositionInString + 1;

                break;
            }
        }
    }
    if (isSumToHigh)
    {
        Console.WriteLine();
        Console.WriteLine("Total sum was to high, but here are all found matches.");
    }
    else
    {
        Console.WriteLine();
        Console.WriteLine($"Total = {totalSum}");
    }

}