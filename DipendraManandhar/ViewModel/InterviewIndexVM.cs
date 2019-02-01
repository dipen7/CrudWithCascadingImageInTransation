using DipendraManandhar.Models;
using System.Collections.Generic;

namespace DipendraManandhar.ViewModel
{
    public class InterviewIndexVM
    {
        public InterviewIndexVM()
        {
            indexlist = new List<Interview>();
            droplist = new List<InterviewDrop>();
            CountryList = new List<Country>();
            StateList = new List<State>();
        }
        public IEnumerable<Interview> indexlist { get; set; }
        public IEnumerable<InterviewDrop> droplist { get; set; }
        public IEnumerable<Country> CountryList { get; set; }
        public IEnumerable<State> StateList { get; set; }
    }
}