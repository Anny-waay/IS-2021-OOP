using System.Collections.Generic;
using Isu.Entities;

namespace IsuExtra.Entities
{
    public class GroupTimetable
    {
        private Group _group;
        private List<TimetableForDay> _timetable;
        public GroupTimetable(Group group)
        {
            _group = group;
            _timetable = new List<TimetableForDay>();
        }

        public Group GroupGet => _group;
        public List<TimetableForDay> Timetable => _timetable;
        public void AddLessonToGroupTimetable(Days day, string nameOfLesson, string startTime, string finishTime, string teacher, string audience)
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