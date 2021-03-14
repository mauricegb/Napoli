using Napoli.Application.Extensions.Input;
using System;
using Xunit;

namespace Napoli.Test.Extensions.Input
{
    public class YesNoParserTest
    {
        [Theory]
        [InlineData(@"y", true)]
        [InlineData(@"n", false)]
        [InlineData(@"Y", true)]
        [InlineData(@"N", false)]
        public void ShouldReturnExpectedResult_IfInputValid(string input, bool expectedResult)
        {
            Assert.Equal(expectedResult, input.ToBool());
        }

        [Theory]
        [InlineData("yes")]
        [InlineData("sejfbjshefv")]
        public void ShouldThrowArgumentException_IfInputNotValid(string input)
        {
            var ex = Assert.Throws<ArgumentException>(() => input.ToBool());

            Assert.Equal($"The input '{input}' was not in the correct format (y/n). (Parameter '{input}')", ex.Message);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void ShouldThrowArgumentException_IfInputNullOrEmpty(string input)
        {
            var ex = Assert.Throws<ArgumentNullException> (() => input.ToBool());

            Assert.Contains("The (y/n) input was null or empty, please remember to enter a value.", ex.Message);
            Assert.Equal(nameof(input), ex.ParamName);
        }
    }
}
