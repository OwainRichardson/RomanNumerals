using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NumeralLogic
{
    public class NumeralService
    {
        // These are the only numerals needed to restrict my inputs between 1 and 3999
        public readonly Dictionary<int, string> _numerals = new Dictionary<int, string>
        {
            { 1, "I" },
            { 5, "V" },
            { 10, "X" },
            { 50, "L" },
            { 100, "C" },
            { 500, "D" },
            { 1000, "M" }
        };

        public void HandleInput()
        {
            // Read the input - ask for a comma separated string
            Console.Write("Please enter the numbers to translate as a comma separated list: ");
            var userInput = Console.ReadLine();
            HandleInput(userInput);
        }

        public List<string> HandleInput(string userInput)
        {
            // Split the string in to individual numbers
            var numberStrings = userInput.Split(',');
            var methodOutput = new List<string>();

            // Loop through all inputs to convert them to numerals
            foreach (var number in numberStrings)
            {
                var stringToReturn = string.Empty;
                var outNumber = 0;

                if (int.TryParse(number, out outNumber))
                {
                    if (outNumber < 1 || outNumber > 3999)
                    {
                        stringToReturn = $"The supplied number, {number}, is not within the allowed range, please enter a number between 1 and 3999";
                    }
                    else
                    {
                        stringToReturn = $"{number} = \"{ParseNumberToNumerals(number.Trim())}\"";
                    }
                }
                else
                {
                    stringToReturn = $"{number} is not a number";
                }

                Console.WriteLine(stringToReturn);
                methodOutput.Add(stringToReturn);
            }

            return methodOutput;
        }

        public string ParseNumberToNumerals(int number)
        {
            return ParseNumberToNumerals(number.ToString());
        }

        public string ParseNumberToNumerals(string number)
        {
            // Split the string into constituent characters - in this case this will yield single digit numbers
            var numberArray = number.ToCharArray();
            StringBuilder sb = new StringBuilder();

            var index = 1;
            var length = numberArray.Length;
            foreach (var numberEntry in numberArray)
            {
                // I want to multiply each number to get the correct number from the input string
                // e.g. 4102 should split to a list of
                // 4000
                // 100
                // 0
                // 2

                // Calculate the correct power of 10 to multiply each number by
                var power = length - index;
                var numberToConvert = 0d;

                // Multiply by the correct power of 10
                // If power = 0 then it has the same effect as multiplying by 1
                numberToConvert = double.Parse(numberEntry.ToString()) * Math.Pow(10, power);

                // As numerals beginning with 4 or 9 are treated differently, send them to a different method
                if (numberToConvert.ToString().StartsWith("4") || numberToConvert.ToString().StartsWith("9"))
                {
                    ProcessSubtractionNumeral(numberToConvert, sb);
                }
                else
                {
                    ProcessNormalNumeral(numberToConvert, sb);
                }

                index++;
            }

            return sb.ToString();
        }

        private void ProcessNormalNumeral(double numberToConvert, StringBuilder sb)
        {
            // Break the number to convert down by continually removing the highest possible numeral value from it
            while (numberToConvert > 0)
            {
                foreach (var numeral in _numerals.OrderByDescending(x => x.Key))
                {
                    if (numberToConvert - numeral.Key >= 0)
                    {
                        sb.Append(numeral.Value);
                        numberToConvert -= numeral.Key;
                        break;
                    }
                }
            }
        }

        private void ProcessSubtractionNumeral(double numberToConvert, StringBuilder sb)
        {
            var previousKey = 0;
            foreach (var numeral in _numerals.OrderBy(x => x.Key))
            {
                if (numberToConvert - numeral.Key < 0)
                {
                    // Find correct numeral by using NUM =  numeral.Key - numberToConvert
                    // e.g. 5 - IV
                    int numeralIndex = numeral.Key - (int)numberToConvert;
                    sb.Append(_numerals[numeralIndex]);

                    sb.Append(numeral.Value);
                    break;
                }

                previousKey = numeral.Key;
            }
        }
    }
}
