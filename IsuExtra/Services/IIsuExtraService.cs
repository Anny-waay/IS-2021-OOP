using System.Collections.Generic;
using Isu.Entities;
using IsuExtra.Entities;

namespace IsuExtra.Services
{
    public interface IIsuExtraService
    {
        OGNP CreateOGNP(char megaFaculty, string nameOfOGNP);
        FlowOGNP CreateFlowOGNP(OGNP ognp, string nameOfFlow, int maxNumberOfStudents);
        GroupTimetable AddTimetableForGroup(Group group);
        void AddStudentToOGNP(StudentExtra student, FlowOGNP flowOGNP);
        void DeleteStudentFromOGNP(StudentExtra student, FlowOGNP flowOGNP);
        List<FlowOGNP> GetListOfFlows(OGNP ognp);
        List<StudentExtra> GetStudentsFromFlowOGNP(FlowOGNP flowOGNP);
        List<Student> GetStudentsWithoutOGNP(Group group);
    }
}
