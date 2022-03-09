using Isu.Tools;
namespace Isu.Entities
{
    public class GroupName
    {
        private char _specilization;
        private int _courseNumber;
        private int _groupNumber;
        public GroupName(string name)
        {
            const int maxCourse = 4;
            const int maxGroupNumber = 15;
            int courseNumber = int.Parse(name[2].ToString());
            int groupNumber = int.Parse(name[3..4]);
            if (name[0] > 'A'
                && courseNumber <= maxCourse
                && groupNumber <= maxGroupNumber)
            {
                _specilization = name[0];
                _courseNumber = courseNumber;
                _groupNumber = groupNumber;
            }
            else
            {
                throw new IsuException("Incorrect name of group");
            }
        }

        public char Specilization => _specilization;
        public int CourseNumber => _courseNumber;
        public int GroupNumber => _groupNumber;
    }
}
