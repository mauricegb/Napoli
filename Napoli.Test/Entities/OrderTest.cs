using Napoli.Entities;
using Napoli.Entities.Enums;
using Napoli.Entities.Interfaces;
using System.Collections.Generic;
using Xunit;

namespace Napoli.Test.Entities
{
    public class OrderTest
    {
        [Fact]
        public void OrderConstructor_ShouldInitialiseDictionaryAsExpected()
        {
            IOrder order = new Order();

            Assert.NotNull(order.Courses);
            Assert.Empty(order.Courses);
        }

        [Fact]
        public void AddCourse_ShouldAddCourseToOrder()
        {
            IOrder order = new Order();

            var courseType = CourseType.Starter;
            const string courseName = "test name";

            ICourse course = new Course(courseType, courseName);
            var courseId = order.AddCourse(course);

            var orderCourses = order.Courses;

            Assert.Contains(orderCourses, p => p.Key.Equals(courseId)
                        && p.Value.CourseType.Equals(courseType)
                        && p.Value.Name.Equals(courseName));
        }

        [Fact]
        public void AddCourse_ShouldReturnRandomId()
        {
            IOrder order = new Order();
            ICourse course = new Course(CourseType.Starter, "test name");

            var courseIdA = order.AddCourse(course);
            var courseIdB = order.AddCourse(course);

            Assert.IsType<int>(courseIdA);
            Assert.IsType<int>(courseIdB);

            Assert.NotEqual(courseIdA, courseIdB);
        }

        [Fact]
        public void UpdateCourse_ShouldUpdateExistingCourse()
        {
            IOrder order = new Order();
            var originalCourseType = CourseType.Starter;
            const string originalCourseName = "original test name";
            ICourse originalCourse = new Course(originalCourseType, originalCourseName);

            var courseId = order.AddCourse(originalCourse);

            var updatedCourseType = CourseType.Starter;
            const string updatedCourseName = "updated test name";
            ICourse updatedCourse = new Course(updatedCourseType, updatedCourseName);

            order.UpdateCourse(updatedCourse, courseId);
            var orderCourses = order.Courses;

            Assert.Contains(orderCourses, p => p.Key.Equals(courseId)
                        && p.Value.CourseType.Equals(updatedCourseType)
                        && p.Value.Name.Equals(updatedCourseName));

            Assert.DoesNotContain(orderCourses, p => p.Key.Equals(courseId)
                        && p.Value.CourseType.Equals(originalCourseType)
                        && p.Value.Name.Equals(originalCourseName));
        }

        [Fact]
        public void UpdateCourse_ShouldThrowException_IfDoesNotExist()
        {
            IOrder order = new Order();
            const int nonExistentOrderId = 452342453;

            var ex = Assert.Throws<KeyNotFoundException>(() => order.UpdateCourse(null, nonExistentOrderId));
            Assert.Equal("Course does not exist", ex.Message);
        }

        [Fact]
        public void DeleteCourse_ShouldRemoveCourseFromOrder()
        {
            IOrder order = new Order();
            ICourse course = new Course(CourseType.Starter, "test name");

            var courseId = order.AddCourse(course);
            order.DeleteCourse(courseId);

            Assert.Empty(order.Courses);
        }

        [Fact]
        public void DeleteCourse_ShouldThrowException_IfDoesNotExist()
        {
            IOrder order = new Order();

            const int nonExistentOrderId = 45234453;

            var ex = Assert.Throws<KeyNotFoundException>(() => order.DeleteCourse(nonExistentOrderId));
            Assert.Equal("Course does not exist", ex.Message);
        }
    }
}
