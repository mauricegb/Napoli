using Napoli.Entities.Enums;
using Napoli.Entities.Interfaces;

namespace Napoli.Entities
{
    public class Course : ICourse
    {
        public string Name { get; set; }

        public CourseType CourseType { get; }

        public Course(CourseType courseType, string name)
        {
            CourseType = courseType;
            Name = name;
        }
    }
}
