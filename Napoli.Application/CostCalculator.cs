using Napoli.Entities;
using Napoli.Entities.Enums;
using Napoli.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Napoli.Application
{
    public static class CostCalculator
    {
        public static double CalculateOrderCost(IOrder order)
        {
            var courses = order.Courses;

            var totalStartersNumber = GetTotalNumberOfCourseType(courses, CourseType.Starter);
            var totalMainsNumber = GetTotalNumberOfCourseType(courses, CourseType.Main);

            var totalStartersCost = GetTotalCourseCost(totalStartersNumber, CourseType.Starter);
            var totalMainsCost = GetTotalCourseCost(totalMainsNumber, CourseType.Main);

            var totalOrderCost = totalStartersCost + totalMainsCost;
            return totalOrderCost;
        }

        private static int GetTotalNumberOfCourseType(Dictionary<int, Course> courses, CourseType courseType)
        {
            return courses.Count(p => p.Value.CourseType.Equals(courseType));
        }

        private static double GetTotalCourseCost(int numberOfCourses, CourseType courseType)
        {
            const double starterCost = 4.4;
            const double mainCost = 7;

            if (courseType.Equals(CourseType.Starter))
                return starterCost * numberOfCourses;

            if (courseType.Equals(CourseType.Main))
                return mainCost * numberOfCourses;

            throw new InvalidOperationException($"Unrecognised {nameof(courseType)}: {courseType}");
        }
    }
}
