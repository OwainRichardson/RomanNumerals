using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RomanNumerals
{
    public class Program
    {
        // There are more numerals than this for larger numbers, but these are the basic ones used
        public static readonly Dictionary<int, string> _numerals = new Dictionary<int, string>
        {
            { 1, "I" },
            { 5, "V" },
            { 10, "X" },
            { 50, "L" },
            { 100, "C" },
            { 500, "D" },
            { 1000, "M" }
        };

        public static void Main(string[] args)
        {
            Console.Write("Please enter the numbers to translate as a comma separated list: ");
            var numberString = Console.ReadLine();
            var numberStringList = numberString.Split(',');

            foreach (var number in numberStringList)
            {
                ParseNumberToNumerals(number.Trim());
            }
        }

        public static void ParseNumberToNumerals(string number)
        {
            var numberArray = number.ToCharArray();
            StringBuilder sb = new StringBuilder();

            var index = 1;
            var length = numberArray.Length;
            foreach (var thing in numberArray)
            {
                var power = length - index;
                var numberToConvert = 0d;

                if (power > 0)
                {
                    numberToConvert = double.Parse(thing.ToString()) * Math.Pow(10, power);
                }
                else
                {
                    numberToConvert = double.Parse(thing.ToString());
                }

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

            Console.WriteLine(sb);
        }

        public static void ProcessNormalNumeral(double numberToConvert, StringBuilder sb)
        {
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

        public static void ProcessSubtractionNumeral(double numberToConvert, StringBuilder sb)
        {
            var previousKey = 0;
            foreach (var numeral in _numerals.OrderBy(x => x.Key))
            {
                if (numberToConvert - numeral.Key < 0)
                {
                    //Find Correct numeral where numeral.Key - NUM = numberToConvert
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
