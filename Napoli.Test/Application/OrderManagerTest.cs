using Napoli.Application;
using Napoli.Entities;
using Napoli.Entities.Enums;
using System;
using System.Collections.Generic;
using Xunit;

namespace Napoli.Test.Application
{
    public class OrderManagerTest
    {
        [Fact]
        public void AddAValidCourse_ShouldReturnRandomIntId()
        {
            var order = new Order();
            const string courseName = "my course name";
            const string courseType = "main";

            int idA = OrderManager.AddCourse(courseName, courseType, order);
            int idB = OrderManager.AddCourse(courseName, courseType, order);

            Assert.NotEqual(idA, idB);
        }

        [Fact]
        public void AddAnInvalidCourse_ShouldThrowArgumentException()
        {
            var order = new Order();
            const string courseName = "my course name";
            const string courseType = "dessert";

            var ex = Assert.Throws<ArgumentException>(() => OrderManager.AddCourse(courseName, courseType, order));
            Assert.Contains($"The input for course type of {courseType} was not a valid value", ex.Message);
        }

        [Fact]
        public void AddAValidCourse_ShouldAddCourseToOrder()
        {
            var order = new Order();
            const string courseName = "my course name";
            const string courseType = "main";

            var courseId = OrderManager.AddCourse(courseName, courseType, order);

            var courses = order.Courses;
            Assert.Equal(CourseType.Main, courses[courseId].CourseType);
            Assert.Equal(courseName, courses[courseId].Name);
        }

        [Fact]
        public void RemoveACourse_ShouldRemoveCourse()
        {
            var order = new Order();
            const string courseName = "my course name";
            const string courseType = "main";
            var courseId = OrderManager.AddCourse(courseName, courseType, order);

            OrderManager.RemoveCourse(courseId.ToString(), order);

            Assert.False(order.Courses.ContainsKey(courseId));
        }

        [Fact]
        public void RemoveANonExistentCourse_ShouldThrow()
        {
            var order = new Order();

            var ex = Assert.Throws<KeyNotFoundException>(() => OrderManager.RemoveCourse("23542352", order));
            Assert.Contains("Course does not exist", ex.Message);
        }

        [Fact]
        public void EditAValidCourse_ShouldReturnSameId()
        {
            var order = new Order();
            const string originalCourseName = "my course name";
            const string originalCourseType = "main";
            var courseId = OrderManager.AddCourse(originalCourseName, originalCourseType, order);

            const string newCourseName = "my new course name";
            const string newCourseType = "starter";
            var editedCourseId = OrderManager.EditCourse(courseId.ToString(), newCourseName, newCourseType, order);

            Assert.Equal(courseId, editedCourseId);
        }

        [Fact]
        public void EditANonExistentCourse_ShouldThrow()
        {
            var order = new Order();

            const string newCourseName = "my new course name";
            const string newCourseType = "starter";

            var ex = Assert.Throws<KeyNotFoundException>(() => OrderManager.EditCourse("3242423", newCourseName, newCourseType, order));
            Assert.Contains("Course does not exist", ex.Message);
        }

        [Fact]
        public void EditACourse_MissingCourseTypeShouldThrow()
        {
            var order = new Order();

            const string newCourseName = "my new course name";

            var ex = Assert.Throws<ArgumentNullException>(() => OrderManager.EditCourse("3242423", newCourseName, string.Empty, order));
            Assert.Contains("The input for course type was null or empty, please remember to enter a value", ex.Message);
        }

        [Fact]
        public void EditAValidCourse_ShouldEditCourse()
        {
            var order = new Order();
            const string originalCourseName = "my course name";
            const string originalCourseType = "main";
            var courseId = OrderManager.AddCourse(originalCourseName, originalCourseType, order);

            const string newCourseName = "my new course name";
            const string newCourseType = "starter";
            var editedCourseId = OrderManager.EditCourse(courseId.ToString(), newCourseName, newCourseType, order);

            var courses = order.Courses;
            Assert.Equal(CourseType.Starter, courses[editedCourseId].CourseType);
            Assert.Equal(newCourseName, courses[editedCourseId].Name);
        }
    }
}
