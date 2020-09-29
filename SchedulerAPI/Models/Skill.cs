using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchedulerAPI.Models
{
    public class Skill
    {
        // Skill Id
        public int SkillId { get; set; }
        // Skill Name, required at creation
        [Required]
        [StringLength(50)]
        public String Name { get; set; }
        // Skill Description, optional
        public String Description { get; set; }
        // Creation Date
        [DataType(DataType.Date)]
        public DateTime CreationDate { get; set; }
        // Last Modified Date
        [DataType(DataType.Date)]
        public DateTime LastModified { get; set; }

        // Navigation Properties
        public IList<SkillAssignment> SkillAssignments { get; set; }
    }
}
