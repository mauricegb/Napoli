using Napoli.Entities.Enums;

namespace Napoli.Entities.Interfaces
{
    public interface ICourse
    {
        string Name { get; set; }

        CourseType CourseType { get; }
    }
}
