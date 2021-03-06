using System;
using Napoli.Application.Extensions.Input;
using Napoli.Entities;
using Napoli.Entities.Interfaces;


namespace Napoli.Application
{
    public static class OrderManager
    {
        public static int AddCourse(string courseName, string courseType, IOrder order)
        {
            var processedCourseType = courseType.ToCourseType();

            var course = new Course(processedCourseType, courseName);

            var courseId = order.AddCourse(course);
            return courseId;
        }

        public static void RemoveCourse(string courseId, IOrder order)
        {
            if (!int.TryParse(courseId, out int processedCourseId))
            {
                throw new ArgumentException($"The courseId input {courseId} was not a valid integer.",
                    nameof(courseId));
            }

            order.DeleteCourse(processedCourseId);
        }

        public static int EditCourse(string courseId, string courseName, string courseType, IOrder order)
        {
            var processedCourseType = courseType.ToCourseType();

            if (!int.TryParse(courseId, out int processedCourseId))
            {
                throw new ArgumentException($"The courseId input {courseId} was not a valid integer.",
                    nameof(courseId));
            }

            var course = new Course(processedCourseType, courseName);

            return order.UpdateCourse(course, processedCourseId);
        }
    }
}
