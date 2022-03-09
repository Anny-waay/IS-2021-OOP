using System.Collections.Generic;
using Isu.Entities;

namespace IsuExtra.Entities
{
    public class StudentExtra
    {
        private Student _student;
        private List<FlowOGNP> _flowOGNPs;
        public StudentExtra(Student student)
        {
            _student = student;
            _flowOGNPs = new List<FlowOGNP>();
        }

        public Student Student => _student;
        public List<FlowOGNP> FlowOGNP => _flowOGNPs;

        public void AddFlowOGNP(FlowOGNP flowOGNP)
        {
            _flowOGNPs.Add(flowOGNP);
        }

        public void DeleteFlowOGNP(FlowOGNP flowOGNP)
        {
            _flowOGNPs.Remove(flowOGNP);
        }
    }
}
