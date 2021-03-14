using Napoli.Entities.Enums;
using System;


namespace Napoli.Application.Extensions.Input
{
    public static class CourseTypeParser
    {
        public static CourseType ToCourseType(this string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                throw new ArgumentNullException(nameof(input), "The input for course type was null or empty, please remember to enter a value.");
            }

            if (!Enum.TryParse(input, ignoreCase: true, out CourseType courseType))
            {
                throw new ArgumentException($"The input for course type of {input} was not a valid value: {Enum.GetNames(typeof(CourseType))}", nameof(input));
            }

            return courseType;
        }
    }
}
