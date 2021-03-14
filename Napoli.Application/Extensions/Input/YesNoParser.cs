using System;

namespace Napoli.Application.Extensions.Input
{
    public static class YesNoParser
    {
        public static bool ToBool(this string input)
        {
            const string yesInputFlag = "y";
            const string noInputFlag = "n";

            if (string.IsNullOrEmpty(input))
            {
                throw new ArgumentNullException(nameof(input), "The (y/n) input was null or empty, please remember to enter a value.");
            }
            else if (input.Equals(yesInputFlag, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            else if (input.Equals(noInputFlag, StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }
            else
            {
                throw new ArgumentException($"The input '{input}' was not in the correct format ({yesInputFlag}/{noInputFlag}).", input);
            }
        }
    }
}
