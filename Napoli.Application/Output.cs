using Napoli.Entities;
using Napoli.Entities.Interfaces;
using System.Collections.Generic;

namespace Napoli.Application
{
    public static class Output
    {
        public static List<string> GetOrderSummary(IOrder order)
        {
            var outputList = new List<string>();
            var courses = order.Courses;

            foreach(KeyValuePair<int, Course> m in courses)
            {
                var summaryLine = $"Course Name: {m.Value.Name} | Course Type: {m.Value.CourseType} | Course Id: {m.Key}";
                outputList.Add(summaryLine);

            }

            return outputList;
        }
    }
}
