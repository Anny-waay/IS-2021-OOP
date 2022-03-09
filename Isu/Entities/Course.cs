using System.Collections.Generic;
namespace Isu.Entities
{
    public class Course
    {
        private const int MaxCourse = 4;
        private int _courseNumber;
        private List<Group> _groups;
        public Course(int courseNumber)
        {
            if (courseNumber <= MaxCourse)
            {
                _courseNumber = courseNumber;
            }

            _groups = new List<Group>();
        }

        public int CourseNumber => _courseNumber;
        public List<Group> Groups => _groups;
        public void AddGroupToCourse(Group group)
        {
            _groups.Add(group);
        }
    }
}
