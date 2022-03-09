using Isu.Services;
using Isu.Tools;
using Isu.Entities;
using NUnit.Framework;

namespace Isu.Tests
{
    public class Tests
    {
        private IIsuService _isuService;

        [SetUp]
        public void Setup()
        {
            //TODO: implement
            _isuService = new IsuService();
        }

        [Test]
        public void AddStudentToGroup_StudentHasGroupAndGroupContainsStudent()
        {
            Group M3203 = _isuService.AddGroup("M3203");
            Student KomovaAnna = _isuService.AddStudent(M3203, "Komova Anna");
            if (_isuService.FindGroup("M3203") == null)
            {
                Assert.Fail("There is no one group");
            }
            if (KomovaAnna.GroupGetter != M3203)
            {
                Assert.Fail("Incorrect group of student");
            }
        }

        [Test]
        public void ReachMaxStudentPerGroup_ThrowException()
        {
            Group M3200 = _isuService.AddGroup("M3200");
            for (int i = 0; i < 20; i++)
            {
                _isuService.AddStudent(M3200, "Gumin Danil");
            }
            Assert.Catch<IsuException>(() =>
            {
                _isuService.AddStudent(M3200, "Raskov Mikhail");
            });
        }

        [Test]
        public void CreateGroupWithInvalidName_ThrowException()
        {
            Assert.Catch<IsuException>(() =>
            {
                _isuService.AddGroup("N3640");
            });
        }

        [Test]
        public void TransferStudentToAnotherGroup_GroupChanged()
        {
            Group M3206 = _isuService.AddGroup("M3206");
            Student BalaginArtem = _isuService.AddStudent(M3206, "Balagin Artem");
            Group M3201 = _isuService.AddGroup("M3201");
            _isuService.ChangeStudentGroup(BalaginArtem, M3201);
            if (BalaginArtem.GroupGetter != M3201)
            {
                Assert.Fail("Group didn't change");
            }
        }
    }
}
