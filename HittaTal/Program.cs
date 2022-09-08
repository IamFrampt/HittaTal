Console.Write("Inmatning: ");
string input = "29535123p48723487597645723645";
Console.WriteLine();

UInt64 totalSum = 0;
string tempWord = string.Empty;
bool isLetterPresent = false;
int row = 1;
CheckIfSameDigit(input);

Console.WriteLine();
Console.WriteLine("Totalt = " + totalSum);


void CheckIfSameDigit(string input)
{
    for (int i = 0; i < input.Length; i++)
    {
        for (int j = i + 1; j < input.Length; j++)
        {
            if (input[i] == input[j])
            {
                tempWord = input.Substring(i, j - i + 1);
                isLetterPresent = tempWord.Any(c => char.IsLetter(c));

                if (!isLetterPresent)
                {
                    totalSum += Convert.ToUInt64(tempWord);
                    Console.Write($"{row.ToString().PadRight(3)}: {input.Substring(0, i)}");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(input.Substring(i, j - i + 1));
                    tempWord = input.Substring(i, j - i + 1);
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine(input.Substring(j + 1, input.Length - (j + 1)));
                    row++;
                }
                break;
            }
        }
    }
}


void PrintResultOfInput()
{

}