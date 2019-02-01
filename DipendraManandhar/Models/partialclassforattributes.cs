using System;
using System.ComponentModel.DataAnnotations;

namespace DipendraManandhar.Models
{
    public class partialclassforattributes
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string MobileNo { get; set; }
        [Required]
        public string Email { get; set; }
        public string Hobby { get; set; }
        [Required]
        public int DropDown { get; set; }
        [Required]
        public string Message { get; set; }
        public string Photolink { get; set; }
        [Required]
        public Nullable<int> Country { get; set; }
        [Required]
        public Nullable<int> State { get; set; }

        public virtual InterviewDrop InterviewDrop { get; set; }
    }

}