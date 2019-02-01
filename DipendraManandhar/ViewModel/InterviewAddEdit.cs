using DipendraManandhar.Models;
using System.Collections.Generic;

namespace DipendraManandhar.ViewModel
{
    public class InterviewAddEdit
    {
        public InterviewAddEdit()
        {
            InterviewObj = new Interview();
            Droplist = new List<InterviewDrop>();
            CountryList = new List<Country>();
            StateList = new List<State>();
        }
        public Interview InterviewObj { get; set; }
        public IEnumerable<InterviewDrop> Droplist { get; set; }
        public IEnumerable<Country> CountryList { get; set; }
        public IEnumerable<State> StateList { get; set; }
    }
}