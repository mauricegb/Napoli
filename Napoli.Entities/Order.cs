using Napoli.Entities.Interfaces;
using System;
using System.Collections.Generic;

namespace Napoli.Entities
{
    public class Order : IOrder
    {
        public Dictionary<int, Course> Courses { get; }

        public Order()
        {
            Courses = new Dictionary<int, Course>();
        }

        public int AddCourse(ICourse course)
        {
            var random = new Random();
            int id = random.Next();

            Courses.Add(id, (Course)course);
            return id;
        }

        public int UpdateCourse(ICourse course, int id)
        {
            if (!Courses.ContainsKey(id))
            {
                throw new KeyNotFoundException("Course does not exist");
            }

            Courses[id] = (Course)course;

            return id;
        }

        public void DeleteCourse(int id)
        {
            if (!Courses.ContainsKey(id))
            {
                throw new KeyNotFoundException("Course does not exist");
            }

            Courses.Remove(id);
        }
    }
}
