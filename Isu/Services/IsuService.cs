using System.Collections.Generic;
using Isu.Entities;
using Isu.Tools;

namespace Isu.Services
{
    public class IsuService : IIsuService
    {
        private const int MaxStudentsNumber = 20;
        private List<Course> _courses;
        private List<Student> _allStudents;
        public IsuService()
        {
            _courses = new List<Course>();
            _allStudents = new List<Student>();
            _courses.Add(new Course(1));
            _courses.Add(new Course(2));
            _courses.Add(new Course(3));
            _courses.Add(new Course(4));
        }

        public Group AddGroup(string name)
        {
            var resultGroup = new Group(name);
            foreach (Group group in _courses[resultGroup.GroupNameGetter.CourseNumber - 1].Groups)
            {
                if (group == resultGroup)
                {
                    throw new IsuException("You already have this group");
                }
            }

            _courses[resultGroup.GroupNameGetter.CourseNumber - 1].AddGroupToCourse(resultGroup);
            return resultGroup;
        }

        public Student AddStudent(Group group, string name)
        {
            if (group.Students.Count == MaxStudentsNumber)
            {
                throw new IsuException("You reach max number of students in this group");
            }

            var resultStudent = new Student(name);
            resultStudent.AddChangeGroupToStudent(group);
            _allStudents.Add(resultStudent);
            group.AddStudentToGroup(resultStudent);
            return resultStudent;
        }

        public Group FindGroup(string groupName)
        {
            var exampleGroup = new Group(groupName);
            foreach (Group group in _courses[exampleGroup.GroupNameGetter.CourseNumber - 1].Groups)
            {
                if (group.GroupNameGetter.GroupNumber == exampleGroup.GroupNameGetter.GroupNumber)
                {
                    return group;
                }
            }

            return null;
        }

        public List<Group> FindGroups(Course course)
        {
            List<Group> groups = course.Groups;
            return groups;
        }

        public Student FindStudent(string name)
        {
            foreach (Student student in _allStudents)
            {
                if (student.Name == name)
                    return student;
            }

            return null;
        }

        public List<Student> FindStudents(string groupName)
        {
            var exampleGroup = new Group(groupName);
            foreach (Group group in _courses[exampleGroup.GroupNameGetter.CourseNumber - 1].Groups)
            {
                if (group.GroupNameGetter.GroupNumber == exampleGroup.GroupNameGetter.GroupNumber)
                {
                    return group.Students;
                }
            }

            return null;
        }

        public List<Student> FindStudents(Course course)
        {
            var students = new List<Student>();
            foreach (Group group in course.Groups)
            {
                foreach (Student student in group.Students)
                {
                    students.Add(student);
                }
            }

            return students;
        }

        public Student GetStudent(int id)
        {
            foreach (Student student in _allStudents)
            {
                if (student.Id == id)
                    return student;
            }

            return null;
        }

        public void ChangeStudentGroup(Student student, Group newGroup)
        {
            if (student.GroupGetter == newGroup)
            {
                throw new IsuException("Old group = new group");
            }

            if (student.GroupGetter == null)
            {
                throw new IsuException("This student doesn't have group, use method AddStudent");
            }

            foreach (Group group in _courses[student.GroupGetter.GroupNameGetter.CourseNumber - 1].Groups)
            {
                if (student.GroupGetter.GroupNameGetter.GroupNumber == group.GroupNameGetter.GroupNumber)
                {
                    group.DeleteStudentFromGroup(student);
                }
            }

            student.AddChangeGroupToStudent(newGroup);
            newGroup.AddStudentToGroup(student);
        }
    }
}
