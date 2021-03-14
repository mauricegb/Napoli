using System;
using System.Linq;


namespace Napoli.Application.Extensions.Input
{
    public static class SummaryDecisionParser
    {
        public static int ToDecisionNumber(this string input)
        {
            string[] validInput = { "1", "2", "3", "4" };

            if (string.IsNullOrEmpty(input))
            {
                throw new ArgumentNullException
                    (nameof(input), "The input was null or empty, please remember to enter a value.");
            }

            if (!validInput.Contains(input.Trim()))
            {
                throw new ArgumentOutOfRangeException
                    (nameof(input), $"The input entered ({input}) was not valid, please enter a valid value - ({string.Join(',', validInput)})");
            }

            return int.Parse(input);
        }
    }
}
