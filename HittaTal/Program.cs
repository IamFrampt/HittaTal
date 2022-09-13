string userInput = string.Empty;
ulong totalSum;
string[] allFoundMatches;
bool wasStringIncorrectAtLeastOnce = false;
Console.ForegroundColor = ConsoleColor.White;


//------------------Main------------------

do
{
    Console.Write("User input: ");
    userInput = Console.ReadLine();
    Console.WriteLine();

    if (!IsStringValid(userInput))
    {
        wasStringIncorrectAtLeastOnce = true;
        Console.Clear();
        Console.WriteLine($"Input was not valid!, Try again.");
        Console.WriteLine();
    }

} while (!IsStringValid(userInput));

if (wasStringIncorrectAtLeastOnce)
{
    Console.Clear();
    Console.WriteLine($"User input: {userInput}");
    Console.WriteLine();
}

allFoundMatches = CheckingForMatchingNumbers(userInput);
totalSum = AddsAllFoundMatches(allFoundMatches);

PrintsResult(userInput, allFoundMatches, totalSum);


//------------------Metoder------------------

string[] CheckingForMatchingNumbers(string userInput)
{
    string[] allFoundMatches = new string[userInput.Length];

    for (int currentNumber = 0; currentNumber < userInput.Length; currentNumber++)
    {
        for (int secondNumber = currentNumber + 1; secondNumber < userInput.Length; secondNumber++)
        {
            if (char.IsDigit(userInput[currentNumber]) && char.IsDigit(userInput[secondNumber]) &&
                userInput.Substring(secondNumber, userInput.Length - secondNumber).Contains(userInput[currentNumber]))
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

    //Tar bort alla index i arrayen som inte har något värde
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

bool IsStringValid(string userInput)
{
    for (int currentIndex = 0; currentIndex < userInput.Length; currentIndex++)
    {
        if (userInput.Substring(currentIndex + 1, userInput.Length - currentIndex - 1).Contains(userInput[currentIndex]) &&
            !userInput.Substring(currentIndex, userInput.Length - currentIndex).Any(c => char.IsLetter(c)))
        {
            return true;
        }
    }

    return false;
}

ulong AddsAllFoundMatches(string[] allFoundMatches)
{
    ulong totalSum = 0;

    for (int i = 0; i < allFoundMatches.Length; i++)
    {
        totalSum += Convert.ToUInt64(allFoundMatches[i]);
    }

    return totalSum;
}

void PrintsResult(string userInput, string[] allFoundMatches, ulong totalSum)
{
    int stringRow = 1;
    int HoldsStringPosition = 0;

    for (int indexLocation = 0; indexLocation < allFoundMatches.Length; indexLocation++)
    {
        for (int currentPositionInString = HoldsStringPosition; currentPositionInString < userInput.Length; currentPositionInString++)
        {
            if (userInput.Substring(currentPositionInString, allFoundMatches[indexLocation].Length).Contains(allFoundMatches[indexLocation]))
            {
                Console.Write($"{stringRow.ToString().PadRight(3)}: {userInput.Substring(0, currentPositionInString)}");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write(userInput.Substring(currentPositionInString, allFoundMatches[indexLocation].Length));
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(userInput.Substring(currentPositionInString + allFoundMatches[indexLocation].Length, userInput.Length - (currentPositionInString + allFoundMatches[indexLocation].Length)));
               
                stringRow++;
                HoldsStringPosition = currentPositionInString + 1;
                
                break;
            }
        }
    }

    Console.WriteLine();
    Console.WriteLine($"Total = {totalSum}");
}