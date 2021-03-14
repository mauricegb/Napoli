using System.Collections.Generic;


namespace Napoli.Entities.Interfaces
{
    public interface IOrder
    {
        Dictionary<int, Course> Courses { get; }

        int AddCourse(ICourse course);

        int UpdateCourse(ICourse course, int id);

        void DeleteCourse(int id);
    }
}
