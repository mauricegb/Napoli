using Napoli.Entities;
using Napoli.Entities.Enums;
using Napoli.Entities.Interfaces;
using Xunit;

namespace Napoli.Test.Entities
{
    public class CourseTest
    {
        [Fact]
        public void CourseConstructor_ShouldAddPropertiesAsExpected()
        {
            var courseType = CourseType.Main;
            const string courseName = "myCourseName";
            ICourse course = new Course(courseType, courseName);

            Assert.Equal(courseType, course.CourseType);
            Assert.Equal(courseName, course.Name);
        }
    }
}
