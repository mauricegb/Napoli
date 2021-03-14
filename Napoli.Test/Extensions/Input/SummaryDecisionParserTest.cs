using Napoli.Application.Extensions.Input;
using System;
using Xunit;

namespace Napoli.Test.Extensions.Input
{
    public class SummaryDecisionParserTest
    {
        [Theory]
        [InlineData("1", 1)]
        [InlineData("2", 2)]
        [InlineData("3", 3)]
        [InlineData(" 3  ", 3)]
        [InlineData("4", 4)]
        public void ReturnIntValue_IfInputValid(string input, int expectedOutput)
        {
            var result = input.ToDecisionNumber();

            Assert.Equal(expectedOutput, result);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void ShouldThrowArgumentNullException_IfInputNullOrEmpty(string input)
        {
            var ex = Assert.Throws<ArgumentNullException>(() => input.ToDecisionNumber());

            Assert.Contains("The input was null or empty, please remember to enter a value.", ex.Message);
            Assert.Equal("input", ex.ParamName);
        }

        [Theory]
        [InlineData("0")]
        [InlineData("5")]
        public void ShouldThrowArgumentOutOfRangeException_IfInputOutsideValidRange(string input)
        {
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => input.ToDecisionNumber());

            Assert.Contains($"The input entered ({input}) was not valid, please enter a valid value - (1,2,3,4)", ex.Message);
            Assert.Equal("input", ex.ParamName);
        }
    }
}
