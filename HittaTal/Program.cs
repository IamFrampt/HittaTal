﻿string userInput = string.Empty;
ulong totalSum;
string[] allFoundMatches;
bool wasStringIncorrectAtLeastOnce = false;
bool isSumToHigh = false;
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
        if (char.IsDigit(userInput[currentIndex]) && userInput.Substring(currentIndex + 1, userInput.Length - currentIndex - 1).Contains(userInput[currentIndex]) &&
            !userInput.Substring(currentIndex, userInput.Length - currentIndex).Any(c => char.IsLetter(c)))
        {
            return true;
        }
    }

    return false;
}

ulong AddsAllFoundMatches(string[] allFoundMatches)
{
    try
    {
        ulong totalSum = 0;

        for (int i = 0; i < allFoundMatches.Length; i++)
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

void PrintsResult(string userInput, string[] allFoundMatches, ulong totalSum)
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