using Napoli.Application.Extensions.Input;
using Napoli.Entities.Enums;
using System;
using Xunit;

namespace Napoli.Test.Extensions.Input
{
    public class CourseTypeParserTest
    {
        [Theory]
        [InlineData("Starter", CourseType.Starter)]
        [InlineData("Main", CourseType.Main)]
        [InlineData("starter", CourseType.Starter)]
        [InlineData("main", CourseType.Main)]
        [InlineData(" Starter", CourseType.Starter)]
        [InlineData("   Main\n    ", CourseType.Main)]
        public void ShouldReturnExpectedResult_IfInputValid(string input, CourseType expectedCourseType)
        {
            Assert.Equal(expectedCourseType, input.ToCourseType());
        }

        [Theory]
        [InlineData("Desert")]
        [InlineData("Starters")]
        public void ShouldThrowArgumentException_IfInputNotValid(string input)
        {
            var ex = Assert.Throws<ArgumentException>(() => input.ToCourseType());

            Assert.Equal($"The input for course type of {input} was not a valid value: {Enum.GetNames(typeof(CourseType))} (Parameter '{nameof(input)}')", ex.Message);
            Assert.Equal("input", ex.ParamName);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void ShouldThrowArgumentException_IfInputNullOrEmpty(string input)
        {
            var ex = Assert.Throws<ArgumentNullException>(() => input.ToCourseType());

            Assert.Contains("The input for course type was null or empty, please remember to enter a value.", ex.Message);
            Assert.Equal("input", ex.ParamName);
        }
    }
}
