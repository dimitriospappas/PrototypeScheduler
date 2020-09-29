using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchedulerAPI.Models
{
    public class SkillAssignment
    {
        // Skill Assignment Id
        public long SkillAssignmentId { get; set; }
        // Skill Assignment Date
        [DataType(DataType.Date)]
        public DateTime AssignmentDate { get; set; }

        // Navigation properties
        [Required]
        public int SkillId { get; set; }
        public Skill Skill { get; set; }
        [Required]
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }
}
