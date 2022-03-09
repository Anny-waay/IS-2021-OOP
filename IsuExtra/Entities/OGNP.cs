using System.Collections.Generic;

namespace IsuExtra.Entities
{
    public class OGNP
    {
        private char _megaFaculty;
        private string _nameOfOGNP;
        private List<FlowOGNP> _flows;
        public OGNP(char megaFaculty, string nameOfOGNP)
        {
            _megaFaculty = megaFaculty;
            _nameOfOGNP = nameOfOGNP;
            _flows = new List<FlowOGNP>();
        }

        public char MegaFaculty => _megaFaculty;
        public string NameOfOGNP => _nameOfOGNP;
        public List<FlowOGNP> Flows => _flows;

        public void AddFlowToOGNP(FlowOGNP flowOGNP)
        {
            _flows.Add(flowOGNP);
        }
    }
}
