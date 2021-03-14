using Napoli.Application;
using Napoli.Application.Extensions.Input;
using Napoli.Entities;
using Napoli.Entities.Interfaces;
using System;
using System.Globalization;

namespace Napoli
{
    public class Program
    {
        public static void Main()
        {
            Console.WriteLine("Hello, welcome to the Napoli Restaurant checkout system.");
            Console.WriteLine("Would you like to create a new order? (y/n)");

            var order = new Order();

            try
            {
                var continueOrdering = Console.ReadLine().ToBool();
                while(continueOrdering)
                {
                    continueOrdering = OrderCourses(order);
                }

                DisplayOrderSummary(order);

                bool finishedSummary;
                do
                {
                    finishedSummary = GetSummaryDecision(order);

                } while (!finishedSummary);

            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
            }

            Console.WriteLine();
            Console.WriteLine("Thank you for using the Napoli Restaurant system, goodbye.");
        }

        private static bool GetSummaryDecision(IOrder order)
        {
            var summaryDecision = ManageOrderQuestion();
            bool finishedSummarising = false;

            switch (summaryDecision)
            {
                case 1:
                    finishedSummarising = true;
                    break;

                case 2:
                    EditItem(order);
                    DisplayOrderSummary(order);
                    break;

                case 3:
                    DeleteItem(order);
                    DisplayOrderSummary(order);
                    break;

                case 4:
                    AddItem(order);
                    DisplayOrderSummary(order);
                    break;
            }

            return finishedSummarising;
        }

        private static bool OrderCourses(IOrder order)
        {
            AddItem(order);

            Console.WriteLine("Have you finished ordering? (y/n)");
            return !Console.ReadLine().ToBool();
        }

        private static void AddItem(IOrder order)
        {
            Console.WriteLine("Please enter course type: (starter/main)");
            var courseType = Console.ReadLine();

            Console.WriteLine("Please enter course name.");
            var courseName = Console.ReadLine();

            OrderManager.AddCourse(courseName, courseType, order);
        }

        private static void EditItem(IOrder order)
        {
            Console.WriteLine("Enter Id of item to edit");
            var courseId = Console.ReadLine();

            Console.WriteLine("Please enter updated course type: (starter/main)");
            var newCourseType = Console.ReadLine();

            Console.WriteLine("Please enter updated course name.");
            var newCourseName = Console.ReadLine();

            OrderManager.EditCourse(courseId, newCourseName, newCourseType, order);
        }

        private static void DeleteItem(IOrder order)
        {
            Console.WriteLine("Enter Id of item to delete");
            var courseId = Console.ReadLine();

            OrderManager.RemoveCourse(courseId, order);
        }

        private static void DisplayOrderSummary(IOrder order)
        {
            Console.WriteLine("This is your order summary:\n");

            var orderSummaryOutput = Output.GetOrderSummary(order);

            foreach(var line in orderSummaryOutput)
            {
                Console.WriteLine(line);
            }

            Console.WriteLine();
            DisplayOrderCost(order);
            Console.WriteLine();
        }

        private static int ManageOrderQuestion()
        {
            Console.WriteLine("Enter 1 to finish ordering, 2 to edit an item, 3 to delete an item, 4 to add another item\n");

            var summaryDecision = Console.ReadLine().ToDecisionNumber();
            return summaryDecision;
        }

        private static void DisplayOrderCost(IOrder order)
        {
            var orderTotalCost = CostCalculator.CalculateOrderCost(order);
            var orderTotalCostOutput = orderTotalCost.ToString("C", CultureInfo.CurrentCulture);

            Console.WriteLine($"The total cost of your order was: {orderTotalCostOutput}\n");
        }
    }
}
