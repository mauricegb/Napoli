using Napoli.Application;
using Napoli.Entities;
using Napoli.Entities.Enums;
using Xunit;

namespace Napoli.Test.Application
{
    public class OutputTest
    {
        [Fact]
        public void GetOrderSummaryForOneCourseOrder_ShouldContainOneLine()
        {
            var order = new Order();
            var courseType = CourseType.Starter;
            const string courseName = "test name";
            var course = new Course(courseType, courseName);
            var courseId = order.AddCourse(course);

            var orderSummary = Output.GetOrderSummary(order);

            Assert.Single(orderSummary);
            Assert.Equal($"Course Name: {courseName} | Course Type: {courseType} | Course Id: {courseId}", 
                orderSummary[0]);
        }

        [Fact]
        public void GetOrderSummaryForMultipleCourseOrder()
        {
            var order = new Order();

            var courseTypeA = CourseType.Starter;
            const string courseNameA = "test name A";
            var courseA = new Course(courseTypeA, courseNameA);
            var courseIdA = order.AddCourse(courseA);

            var courseTypeB = CourseType.Starter;
            const string courseNameB = "test name B";
            var courseB = new Course(courseTypeB, courseNameB);
            var courseIdB = order.AddCourse(courseB);

            var orderSummary = Output.GetOrderSummary(order);

            Assert.Equal(2, orderSummary.Count);
            Assert.Equal($"Course Name: {courseNameA} | Course Type: {courseTypeA} | Course Id: {courseIdA}", orderSummary[0]);
            Assert.Equal($"Course Name: {courseNameB} | Course Type: {courseTypeB} | Course Id: {courseIdB}", orderSummary[1]);
        }

        [Fact]
        public void GetOrderSummaryForEmptyOrder_ShouldBeEmpty()
        {
            var order = new Order();
            var orderSummary = Output.GetOrderSummary(order);

            Assert.Empty(orderSummary);
        }
    }
}
