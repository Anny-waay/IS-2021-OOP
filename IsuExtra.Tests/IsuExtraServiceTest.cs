using Isu.Services;
using Isu.Entities;
using IsuExtra.Tools;
using IsuExtra.Entities;
using IsuExtra.Services;
using NUnit.Framework;
using System.Collections.Generic;

namespace IsuExtra.Tests
{
    public class Tests
    {
        private IIsuService _isuService;
        private IIsuExtraService _isuExtraService;

        [SetUp]
        public void Setup()
        {
            //TODO: implement
            _isuService = new IsuService();
            _isuExtraService = new IsuExtraService();
        }

        [Test]
        public void AddStudentToOGNP_StudentHasOGNPAndOGNPContainsStudent()
        {
            Group M3203 = _isuService.AddGroup("M3203");
            Student KomovaAnna = _isuService.AddStudent(M3203, "Komova Anna");
            GroupTimetable M3203GroupTimetable = _isuExtraService.AddTimetableForGroup(M3203);
            M3203GroupTimetable.AddLessonToGroupTimetable(Days.Monday, "Linal", "10:00", "11:30", "MarinaIvanovna", "321A");
            M3203GroupTimetable.AddLessonToGroupTimetable(Days.Monday, "Linal", "11:40", "13:10", "MarinaIvanovna", "321A");
            M3203GroupTimetable.AddLessonToGroupTimetable(Days.Wednesday, "Linal", "10:00", "11:30", "MarinaIvanovna", "321A");
            M3203GroupTimetable.AddLessonToGroupTimetable(Days.Thursday, "Linal", "10:00", "11:30", "MarinaIvanovna", "321A");
            var KomovaAnnaExtra = new StudentExtra(KomovaAnna);
            OGNP Kiberbez = _isuExtraService.CreateOGNP('N', "Kiberbez");
            FlowOGNP Kib2 = _isuExtraService.CreateFlowOGNP(Kiberbez, "Kib2", 20);
            Kib2.AddOGNPLesson(Days.Wednesday, "Kripto", "13:30", "15:00", "MarinaIvanovna", "325B");
            _isuExtraService.AddStudentToOGNP(KomovaAnnaExtra, Kib2);

            Assert.AreNotEqual(Kib2.Students.Count, 0);
            Assert.AreNotEqual(KomovaAnnaExtra.FlowOGNP.Count, 0);
        }

        [Test]
        public void AddStudentToOGNPOfHisFaculty_ThrowException()
        {
            Group M3204 = _isuService.AddGroup("M3204");
            Student KomovaAnna = _isuService.AddStudent(M3204, "Komova Anna");
            GroupTimetable M3204GroupTimetable = _isuExtraService.AddTimetableForGroup(M3204);
            M3204GroupTimetable.AddLessonToGroupTimetable(Days.Monday, "Linal", "10:00", "11:30", "MarinaIvanovna", "321A");
            M3204GroupTimetable.AddLessonToGroupTimetable(Days.Monday, "Linal", "11:40", "13:10", "MarinaIvanovna", "321A");
            M3204GroupTimetable.AddLessonToGroupTimetable(Days.Wednesday, "Linal", "10:00", "11:30", "MarinaIvanovna", "321A");
            M3204GroupTimetable.AddLessonToGroupTimetable(Days.Thursday, "Linal", "10:00", "11:30", "MarinaIvanovna", "321A");
            var KomovaAnnaExtra = new StudentExtra(KomovaAnna);
            OGNP Math = _isuExtraService.CreateOGNP('M', "Math");
            FlowOGNP Math2 = _isuExtraService.CreateFlowOGNP(Math, "Math2", 20);
            Math2.AddOGNPLesson(Days.Wednesday, "Analys", "10:00", "11:30", "MarinaIvanovna", "325B");

            Assert.Catch<IsuExtraException>(() =>
            {
                _isuExtraService.AddStudentToOGNP(KomovaAnnaExtra, Math2);
            });
        }

        [Test]
        public void DeleteStudentFromOGNP_StudentDoesNotHaveThisOGNP()
        {
            Group M3200 = _isuService.AddGroup("M3200");
            Student KomovaAnna = _isuService.AddStudent(M3200, "Komova Anna");
            GroupTimetable M3200GroupTimetable = _isuExtraService.AddTimetableForGroup(M3200);
            M3200GroupTimetable.AddLessonToGroupTimetable(Days.Monday, "Linal", "10:00", "11:30", "MarinaIvanovna", "321A");
            M3200GroupTimetable.AddLessonToGroupTimetable(Days.Monday, "Linal", "11:40", "13:10", "MarinaIvanovna", "321A");
            M3200GroupTimetable.AddLessonToGroupTimetable(Days.Wednesday, "Linal", "10:00", "11:30", "MarinaIvanovna", "321A");
            M3200GroupTimetable.AddLessonToGroupTimetable(Days.Thursday, "Linal", "10:00", "11:30", "MarinaIvanovna", "321A");
            var KomovaAnnaExtra = new StudentExtra(KomovaAnna);
            OGNP SienceAboutLife= _isuExtraService.CreateOGNP('P', "Sience about life");
            FlowOGNP Sience2 = _isuExtraService.CreateFlowOGNP(SienceAboutLife, "Sience2", 20);
            Sience2.AddOGNPLesson(Days.Wednesday, "Wine", "13:30", "15:00", "MarinaIvanovna", "325B");
            _isuExtraService.AddStudentToOGNP(KomovaAnnaExtra, Sience2);
            _isuExtraService.DeleteStudentFromOGNP(KomovaAnnaExtra, Sience2);

            Assert.AreEqual(Sience2.Students.Count, 0);
            Assert.AreEqual(KomovaAnnaExtra.FlowOGNP.Count, 0);
        }

        [Test]
        public void GetStudentsWithoutOGNP_RightList()
        {
            Group M3206 = _isuService.AddGroup("M3206");
            Student BalaginArtem = _isuService.AddStudent(M3206, "Balagin Artem");
            Student GolyakovaTatiana = _isuService.AddStudent(M3206, "Golyakova Tatiana");
            Student GolyakovaMasha = _isuService.AddStudent(M3206, "Golyakova Masha");
            GroupTimetable M3206GroupTimetable = _isuExtraService.AddTimetableForGroup(M3206);
            M3206GroupTimetable.AddLessonToGroupTimetable(Days.Monday, "Linal", "10:00", "11:30", "MarinaIvanovna", "321A");
            M3206GroupTimetable.AddLessonToGroupTimetable(Days.Monday, "Linal", "11:40", "13:10", "MarinaIvanovna", "321A");
            M3206GroupTimetable.AddLessonToGroupTimetable(Days.Wednesday, "Linal", "10:00", "11:30", "MarinaIvanovna", "321A");
            M3206GroupTimetable.AddLessonToGroupTimetable(Days.Thursday, "Linal", "10:00", "11:30", "MarinaIvanovna", "321A");
            var BalaginArtemExtra = new StudentExtra(BalaginArtem);
            OGNP SienceLife = _isuExtraService.CreateOGNP('P', "Sience  life");
            FlowOGNP Sience1 = _isuExtraService.CreateFlowOGNP(SienceLife, "Sience1", 20);
            Sience1.AddOGNPLesson(Days.Wednesday, "Wine", "13:30", "15:00", "MarinaIvanovna", "325B");
            _isuExtraService.AddStudentToOGNP(BalaginArtemExtra, Sience1);
            var expectedResult = new List<Student>();
            expectedResult.Add(GolyakovaMasha);
            expectedResult.Add(GolyakovaTatiana);
            List<Student> result = _isuExtraService.GetStudentsWithoutOGNP(M3206);
            foreach (Student student in result)
            {
                Assert.AreNotEqual(student, BalaginArtem);
            }
        }
    }
}
