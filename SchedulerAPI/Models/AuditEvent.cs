using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchedulerAPI.Models
{
    public class AuditEvent
    {
        // Audit Event Id
        public long AuditEventId { get; set; }
        // Audit Event Timestamp
        [DataType(DataType.Date)]
        public DateTime EventDate { get; set; }
        // Audit Event Description, required at creation
        [Required]
        public String EventDescription { get; set; }

        // Navigation Properties
        [Required]
        public int EmployeeId { set; get; }
        public Employee Employee { get; set; }
    }
}
