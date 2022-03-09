using System.Collections.Generic;
namespace Isu.Entities
{
    public class Group
    {
        private List<Student> _students;
        private GroupName _groupName;
        public Group(string name)
        {
            _groupName = new GroupName(name);
            _students = new List<Student>();
        }

        public GroupName GroupNameGetter => _groupName;
        public List<Student> Students => _students;

        public void AddStudentToGroup(Student student)
        {
            _students.Add(student);
        }

        public void DeleteStudentFromGroup(Student student)
        {
            _students.Remove(student);
        }
    }
}
