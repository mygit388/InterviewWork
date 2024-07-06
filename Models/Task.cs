using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InterviewWork.Models
{
    public class Task
    {
        
            public int Id { get; set; }
            public int ProfileId { get; set; }
            public string TaskName { get; set; }
            public string TaskDescription { get; set; }
            public DateTime StartTime { get; set; }
            public int Status { get; set; }
            public virtual Profile Profile { get; set; }
        
    }
}