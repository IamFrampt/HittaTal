string userInput = string.Empty;
UInt64 totalSum = 0;
Console.ForegroundColor = ConsoleColor.White;
string[] allFoundMatches;
bool wasStringIncorrectAtLeastOnce = false;

do
{
    //userInput = "29535123p48723487597645723645"; // Test string
    //%555445%%%%554455&55445544554455n554555

    Console.Write("Inmatning: ");
    userInput = Console.ReadLine();
    Console.WriteLine();

    if (!IsStringValid(userInput))
    {
        wasStringIncorrectAtLeastOnce = true;
        Console.Clear();
        Console.WriteLine($"Den inmatade strängen är ogiltig.");
        Console.WriteLine();
    }

} while (!IsStringValid(userInput));

if (wasStringIncorrectAtLeastOnce)
{
    Console.Clear();
    Console.WriteLine($"Inmatning: {userInput}");
    Console.WriteLine();
}

allFoundMatches = CheckingForMatchingNumbers(userInput);
totalSum = AddsAllFoundMatches(CheckingForMatchingNumbers(userInput));

PrintsResult(userInput, allFoundMatches, totalSum);

bool IsStringValid(string userInput)
{
    for (int currentIndex = 0; currentIndex < userInput.Length; currentIndex++)
    {
        if (userInput.Substring(currentIndex+1, userInput.Length - currentIndex-1).Contains(userInput[currentIndex]) &&
            !userInput.Substring(currentIndex, userInput.Length - currentIndex).Any(c => char.IsLetter(c)))
        {
            return true;
        }
    }
    return false;
}

string[] CheckingForMatchingNumbers(string userInput)
{
    string[] allFoundMatches = new string[userInput.Length];

    for (int currentNumber = 0; currentNumber < userInput.Length; currentNumber++)
    {
        for (int secondNumber = currentNumber + 1; secondNumber < userInput.Length; secondNumber++)
        {
            if (userInput.Substring(secondNumber, userInput.Length - secondNumber).Contains(userInput[currentNumber]) &&
                char.IsDigit(userInput[secondNumber]))
            {
                if (userInput[currentNumber] == userInput[secondNumber])
                {
                    allFoundMatches[currentNumber] = userInput.Substring(currentNumber, secondNumber - currentNumber + 1);
                    break;
                }
            }
            else
                break;
        }
    }

    for (int i = 0; i < allFoundMatches.Length; i++)
    {
        if (allFoundMatches[i] == null)
        {
            allFoundMatches = allFoundMatches.Where((source, index) => index != i).ToArray();
            i--;
        }
    }

    return allFoundMatches;
}

ulong AddsAllFoundMatches(string[] allFoundMatches)
{
    ulong totalSum = 0;
    for (int indexLocation = 0; indexLocation < allFoundMatches.Length; indexLocation++)
    {
        totalSum += Convert.ToUInt64(allFoundMatches[indexLocation]);
    }
    return totalSum;
}

void PrintsResult(string userInput, string[] allFoundMatches, ulong totalSum)
{
    int stringRow = 1;
    int HoldsStringPosition = 0;

    for (int indexLocation = 0; indexLocation < allFoundMatches.Length; indexLocation++)
    {
        if (userInput.Contains(allFoundMatches[indexLocation], StringComparison.OrdinalIgnoreCase))
        {
            for (int currentPositionInString = HoldsStringPosition; currentPositionInString < userInput.Length; currentPositionInString++)
            {
                if (userInput.Substring(currentPositionInString, allFoundMatches[indexLocation].Length).Contains(allFoundMatches[indexLocation], StringComparison.OrdinalIgnoreCase))
                {
                    Console.Write($"{stringRow.ToString().PadRight(3)}: {userInput.Substring(0, currentPositionInString)}");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write(userInput.Substring(currentPositionInString, allFoundMatches[indexLocation].Length));
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine(userInput.Substring(currentPositionInString + allFoundMatches[indexLocation].Length, userInput.Length - (currentPositionInString + allFoundMatches[indexLocation].Length)));
                    stringRow++;
                    HoldsStringPosition = currentPositionInString + 1;
                    break;
                }
            }
        }
    }


    Console.WriteLine();
    Console.WriteLine($"Totalt = {totalSum}");
}