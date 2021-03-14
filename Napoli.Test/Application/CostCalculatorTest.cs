using Napoli.Application;
using Napoli.Entities;
using Napoli.Entities.Enums;
using Xunit;

namespace Napoli.Test.Application
{
    public class CostCalculatorTest
    {
        [Fact]
        public void CalculateExpectedOrderCostForOneStarter()
        {
            var order = new Order();
            var starter = new Course(CourseType.Starter, "Calamari");

            order.AddCourse(starter);

            var totalOrderCost = CostCalculator.CalculateOrderCost(order);
            Assert.Equal(4.4, totalOrderCost);
        }

        [Fact]
        public void CalculateExpectedOrderCostForOneMain()
        {
            var order = new Order();
            var main = new Course(CourseType.Main, "Lasagna");

            order.AddCourse(main);

            var totalOrderCost = CostCalculator.CalculateOrderCost(order);
            Assert.Equal(7, totalOrderCost);
        }

        [Fact]
        public void CalculateExpectedOrderCostForEmptyOrder()
        {
            var order = new Order();

            var totalOrderCost = CostCalculator.CalculateOrderCost(order);
            Assert.Equal(0, totalOrderCost);
        }

        [Fact]
        public void CalculateExpectedOrderCostForComplexOrder()
        {
            var order = new Order();

            var starterA = new Course(CourseType.Starter, "Prawn Salad");
            var starterB = new Course(CourseType.Starter, "Tortellini Skewers");
            var starterC = new Course(CourseType.Starter, "Wild Mushroom Arancini");

            var mainA = new Course(CourseType.Main, "Pizza");
            var mainB = new Course(CourseType.Main, "Lasagna");
            var mainC = new Course(CourseType.Main, "Spaghetti");
            var mainD = new Course(CourseType.Main, "Gnocchi");

            order.AddCourse(starterA);
            order.AddCourse(mainD);
            order.AddCourse(mainC);
            order.AddCourse(mainA);
            order.AddCourse(starterB);
            order.AddCourse(mainB);
            order.AddCourse(starterC);

            var totalCalculatedCost = CostCalculator.CalculateOrderCost(order);
            Assert.Equal(41.2, totalCalculatedCost);
        }

        [Fact]
        public void CalculateExpectedOrderCostForComplexOrderWithItemsRemoved()
        {
            var order = new Order();

            var starterA = new Course(CourseType.Starter, "Prawn Salad");
            var starterB = new Course(CourseType.Starter, "Tortellini Skewers");
            var starterC = new Course(CourseType.Starter, "Wild Mushroom Arancini");

            var mainA = new Course(CourseType.Main, "Pizza");
            var mainB = new Course(CourseType.Main, "Lasagna");
            var mainC = new Course(CourseType.Main, "Spaghetti");
            var mainD = new Course(CourseType.Main, "Gnocchi");

            order.AddCourse(starterA);
            order.AddCourse(mainD);
            order.AddCourse(mainC);
            order.AddCourse(mainA);
            var starterBId = order.AddCourse(starterB);
            order.AddCourse(mainB);
            order.AddCourse(starterC);

            order.DeleteCourse(starterBId);

            var totalCalculatedCost = CostCalculator.CalculateOrderCost(order);
            Assert.Equal(36.8, totalCalculatedCost);
        }

        [Fact]
        public void CalculateExpectedOrderCostForComplexOrderWithItemTypeUpdated()
        {
            var order = new Order();

            var starterA = new Course(CourseType.Starter, "Prawn Salad");
            var starterB = new Course(CourseType.Starter, "Tortellini Skewers");
            var starterC = new Course(CourseType.Starter, "Wild Mushroom Arancini");

            var mainA = new Course(CourseType.Main, "Pizza");
            var mainB = new Course(CourseType.Main, "Lasagna");
            var mainC = new Course(CourseType.Main, "Spaghetti");
            var mainD = new Course(CourseType.Main, "Gnocchi");

            order.AddCourse(starterA);
            order.AddCourse(mainD);
            order.AddCourse(mainC);
            order.AddCourse(mainA);
            var starterBId = order.AddCourse(starterB);
            order.AddCourse(mainB);
            order.AddCourse(starterC);

            var starterBModified = new Course(CourseType.Main, "Tortellini Pasta");
            order.UpdateCourse(starterBModified, starterBId);

            var totalCalculatedCost = CostCalculator.CalculateOrderCost(order);
            Assert.Equal(43.8, totalCalculatedCost);
        }
    }
}
