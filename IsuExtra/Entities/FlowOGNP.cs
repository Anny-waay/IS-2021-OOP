using System.Collections.Generic;

namespace IsuExtra.Entities
{
    public class FlowOGNP
    {
        private char _megaFaculty;
        private string _nameOfFlow;
        private int _maxNumberOfStudents;
        private List<TimetableForDay> _timetable;
        private List<StudentExtra> _students;
        public FlowOGNP(string name, int maxNumberOfStudents)
        {
            _nameOfFlow = name;
            _maxNumberOfStudents = maxNumberOfStudents;
            _timetable = new List<TimetableForDay>();
            _students = new List<StudentExtra>();
        }

        public char MegaFaculty => _megaFaculty;
        public string NameOfFlow => _nameOfFlow;
        public int MaxNumberOfStudents => _maxNumberOfStudents;
        public List<TimetableForDay> Timetable => _timetable;
        public List<StudentExtra> Students => _students;

        public void SetMegaFaculty(char megaFaculty)
        {
            _megaFaculty = megaFaculty;
        }

        public void AddStudent(StudentExtra student)
        {
            _students.Add(student);
        }

        public void DeleteStudent(StudentExtra student)
        {
            _students.Remove(student);
        }

        public void AddOGNPLesson(Days day, string nameOfLesson, string startTime, string finishTime, string teacher, string audience)
        {
            foreach (TimetableForDay timetableForDay in _timetable)
            {
                if (timetableForDay.Day == day)
                {
                    timetableForDay.AddLesson(nameOfLesson, startTime, finishTime, teacher, audience);
                    return;
                }
            }

            var newTimetableForDay = new TimetableForDay(day);
            newTimetableForDay.AddLesson(nameOfLesson, startTime, finishTime, teacher, audience);
            _timetable.Add(newTimetableForDay);
        }
    }
}
