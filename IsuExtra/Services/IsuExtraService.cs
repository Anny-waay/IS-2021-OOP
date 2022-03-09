using System.Collections.Generic;
using Isu.Entities;
using IsuExtra.Entities;
using IsuExtra.Tools;

namespace IsuExtra.Services
{
    public class IsuExtraService : IIsuExtraService
    {
        private const int MaxOGNPNumber = 2;
        private List<GroupTimetable> _groupTimetables;
        private List<OGNP> _oGNPs;
        public IsuExtraService()
        {
            _groupTimetables = new List<GroupTimetable>();
            _oGNPs = new List<OGNP>();
        }

        public OGNP CreateOGNP(char megaFaculty, string nameOfOGNP)
        {
            foreach (OGNP ognp in _oGNPs)
            {
                if (ognp.NameOfOGNP == nameOfOGNP)
                {
                    throw new IsuExtraException("You already have this OGNP!");
                }
            }

            var newOGNP = new OGNP(megaFaculty, nameOfOGNP);
            _oGNPs.Add(newOGNP);
            return newOGNP;
        }

        public FlowOGNP CreateFlowOGNP(OGNP ognp, string nameOfFlow, int maxNumberOfStudents)
        {
            foreach (FlowOGNP flowOGNP in ognp.Flows)
            {
                if (flowOGNP.NameOfFlow == nameOfFlow)
                {
                    throw new IsuExtraException("You already have this flow in this OGNP!");
                }
            }

            var newFlowOGNP = new FlowOGNP(nameOfFlow, maxNumberOfStudents);
            newFlowOGNP.SetMegaFaculty(ognp.MegaFaculty);
            ognp.AddFlowToOGNP(newFlowOGNP);
            return newFlowOGNP;
        }

        public GroupTimetable AddTimetableForGroup(Group group)
        {
            foreach (GroupTimetable groupTimetable in _groupTimetables)
            {
                if (Equals(groupTimetable.GroupGet, group))
                {
                    throw new IsuExtraException("You already have timetable for this group!");
                }
            }

            var newTimetable = new GroupTimetable(group);
            _groupTimetables.Add(newTimetable);
            return newTimetable;
        }

        public void AddStudentToOGNP(StudentExtra student, FlowOGNP flowOGNP)
        {
            if (student.Student.GroupGetter.GroupNameGetter.Specilization == flowOGNP.MegaFaculty)
            {
                throw new IsuExtraException("This OGNP is from your faculty!");
            }

            if (student.FlowOGNP.Count == MaxOGNPNumber)
            {
                throw new IsuExtraException("This student already have 2 OGNPs!");
            }

            if (flowOGNP.Students.Count == flowOGNP.MaxNumberOfStudents)
            {
                throw new IsuExtraException("Max number of students in this flow!");
            }

            foreach (TimetableForDay timetableForDay in flowOGNP.Timetable)
            {
                foreach (GroupTimetable groupTimetable in _groupTimetables)
                {
                    if (Equals(groupTimetable.GroupGet, student.Student.GroupGetter))
                    {
                        foreach (TimetableForDay groupTimetableForDay in groupTimetable.Timetable)
                        {
                            if (timetableForDay.Day == groupTimetableForDay.Day)
                            {
                                foreach (Lesson lessonOGNP in timetableForDay.Lessons)
                                {
                                    foreach (Lesson groupLesson in groupTimetableForDay.Lessons)
                                    {
                                        if (lessonOGNP.Starttime == groupLesson.Starttime)
                                        {
                                            throw new IsuExtraException("Intersection in timetable!");
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            student.AddFlowOGNP(flowOGNP);
            flowOGNP.AddStudent(student);
        }

        public void DeleteStudentFromOGNP(StudentExtra student, FlowOGNP flowOGNP)
        {
            student.DeleteFlowOGNP(flowOGNP);
            flowOGNP.DeleteStudent(student);
        }

        public List<FlowOGNP> GetListOfFlows(OGNP ognp)
        {
            List<FlowOGNP> flowsOGNP = ognp.Flows;
            return flowsOGNP;
        }

        public List<StudentExtra> GetStudentsFromFlowOGNP(FlowOGNP flowOGNP)
        {
            List<StudentExtra> students = flowOGNP.Students;
            return students;
        }

        public List<Student> GetStudentsWithoutOGNP(Group group)
        {
            var studentsWithoutOGNP = new List<Student>();
            foreach (Student student in group.Students)
            {
                bool studentHasOGNP = false;
                foreach (OGNP ognp in _oGNPs)
                {
                    foreach (FlowOGNP flowOGNP in ognp.Flows)
                    {
                        foreach (StudentExtra studentExtra in flowOGNP.Students)
                        {
                            if (Equals(student, studentExtra.Student))
                            {
                                studentHasOGNP = true;
                            }
                        }
                    }
                }

                if (!studentHasOGNP)
                {
                    studentsWithoutOGNP.Add(student);
                }
            }

            return studentsWithoutOGNP;
        }
    }
}
