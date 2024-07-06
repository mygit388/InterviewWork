using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InterviewWork.Models
{
    public class Profile
    {
        [Key]
        public int ProfileId { get; set; }
        [Required]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailId { get; set; }
        public virtual ICollection<Task> Tasks { get; set; }
    }
}