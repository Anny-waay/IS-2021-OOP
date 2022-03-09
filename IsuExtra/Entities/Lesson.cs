namespace IsuExtra.Entities
{
    public class Lesson
    {
        private string _nameOfLesson;
        private string _startTime;
        private string _finishTime;
        private string _teacher;
        private string _audience;

        public Lesson(string nameOfLesson, string startTime, string finishTime, string teacher, string audience)
        {
        _nameOfLesson = nameOfLesson;
        _startTime = startTime;
        _finishTime = finishTime;
        _teacher = teacher;
        _audience = audience;
        }

        public string NameOfLesson => _nameOfLesson;
        public string Starttime => _startTime;
        public string FinishTime => _finishTime;
        public string Teacher => _teacher;
        public string Audience => _audience;
    }
}
