using System.Collections.Generic;

namespace IsuExtra.Entities
{
    public class TimetableForDay
    {
        private Days _day;
        private List<Lesson> _lessons;
        public TimetableForDay(Days day)
        {
            _day = day;
            _lessons = new List<Lesson>();
        }

        public Days Day => _day;
        public List<Lesson> Lessons => _lessons;

        public void AddLesson(string nameOfLesson, string startTime, string finishTime, string teacher, string audience)
        {
            _lessons.Add(new Lesson(nameOfLesson, startTime, finishTime, teacher, audience));
        }
    }
}
