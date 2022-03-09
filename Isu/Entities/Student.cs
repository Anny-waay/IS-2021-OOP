namespace Isu.Entities
{
    public class Student
    {
        private static int _idgenerator = 100000;
        private string _name;
        private int _id;
        private Group _group;
        public Student(string name)
        {
            _name = name;
            _id = _idgenerator++;
        }

        public int Id => _id;
        public string Name => _name;
        public Group GroupGetter => _group;

        public void AddChangeGroupToStudent(Group group)
        {
            _group = group;
        }
    }
}