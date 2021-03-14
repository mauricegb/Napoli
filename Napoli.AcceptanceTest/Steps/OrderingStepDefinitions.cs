using FluentAssertions;
using Napoli.Application;
using Napoli.Entities;
using Napoli.Entities.Interfaces;
using System.Collections.Generic;
using TechTalk.SpecFlow;

namespace Napoli.AcceptanceTest.Steps
{
    [Binding]
    public sealed class OrderingStepDefinitions
    {
        private readonly ScenarioContext _scenarioContext;

        public OrderingStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Given(@"an order does not have any courses added to it")]
        public void GivenAnOrderDoesNotHaveAnyCoursesAddedToIt()
        {
            var order = new Order();
            _scenarioContext.Add("order", order);
        }

        [Given("the name of the course is (.*)")]
        public void GivenTheNameOfTheCourseIs(string name)
        {
            _scenarioContext.Add("name", name);
        }

        [Given("the course type is a (.*)")]
        public void GivenTheCourseTypeIs(string courseType)
        {
            _scenarioContext.Add("courseType", courseType);
        }

        [Given("the following courses are added to the order")]
        public void GivenTheFollowingCoursesAreAddedToTheOrder(Table table)
        {
            var order = new Order();

            for (var i = 0; i < table.RowCount; i++)
            {
                var row = table.Rows[i];
                var courseId = OrderManager.AddCourse(row["Name"], row["Course Type"], order);

                _scenarioContext.Add($"courseId{i}", courseId);
            }

            _scenarioContext.Add("order", order);          
        }

        [When("the course is added to the order")]
        public void WhenTheCourseIsAddedToTheOrder()
        {
            var order = new Order();
            var courseId = OrderManager.AddCourse(_scenarioContext["name"].ToString(), _scenarioContext["courseType"].ToString(), order);

            _scenarioContext.Add("order", order);
            _scenarioContext.Add("courseId0", courseId);
        }

        [When("the total cost is calculated")]
        public void WhenTheTotalCostIsCalculated()
        {
            var order = (IOrder)_scenarioContext["order"];
            
            var totalCost = CostCalculator.CalculateOrderCost(order);

            _scenarioContext["totalCost"] = totalCost;
        }

        [Then("the result should be £(.*)")]
        public void ThenTheResultShouldBe(double expectedResult)
        {
            var totalCost = (double)_scenarioContext["totalCost"];
            totalCost.Should().Be(expectedResult);
        }

        [When(@"a summary of the order is requested")]
        public void WhenASummaryOfTheOrderIsRequested()
        {
            var order = (IOrder)_scenarioContext["order"];

            var orderSummary = Output.GetOrderSummary(order);

            _scenarioContext["orderSummary"] = orderSummary;
        }

        [When(@"the course is updated so that the course name is (.*) and the course type is a (.*)")]
        public void WhenTheCourseIsUpdatedSoThatTheCourseNameIsPizzaAndTheCourseTypeIsAMain(string name, string courseType)
        {
            var order = (IOrder)_scenarioContext["order"];
            var courseId = _scenarioContext["courseId0"].ToString();

            OrderManager.EditCourse(courseId, name, courseType, order);
        }

        [When(@"course (.*) is removed from the order")]
        public void WhenCourseIsRemovedFromTheOrder(int courseNumber)
        {
            var order = (IOrder)_scenarioContext["order"];
            var courseId = _scenarioContext[$"courseId{courseNumber-1}"].ToString();

            OrderManager.RemoveCourse(courseId, order);
        }


        [Then(@"one course with the name of (.*) and course type of (.*) and a course id should be displayed in the summary")]
        public void ThenTheCourseShouldBeDisplayedInTheSummary(string name, string courseType)
        {
            var orderSummary = (List<string>)_scenarioContext["orderSummary"];
            var courseId = _scenarioContext["courseId0"];

            orderSummary.Should().ContainSingle();
            orderSummary.Should().Contain(p => p.Contains($"Course Name: {name} | Course Type: {courseType} | Course Id: {courseId}"));
        }

        [Then(@"the summary produced is empty")]
        public void ThenTheSummaryProducedIsEmpty()
        {
            var orderSummary = (List<string>)_scenarioContext["orderSummary"];

            orderSummary.Should().BeEmpty();
        }
    }
}
