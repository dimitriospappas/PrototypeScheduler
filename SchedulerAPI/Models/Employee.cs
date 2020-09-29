using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;

namespace SchedulerAPI.Models
{
    public class Employee
    {
        // Employee Id
        public int EmployeeId { get; set; }
        // Employee Name, required at creation
        [Required]
        [StringLength(50)]
        public String Name { get; set; }
        // Employee Name, required at creation
        [Required]
        [StringLength(50)]
        // Employee Surname, required at creation
        public String Surname { get; set; }
        // Employee Phone number, optional
        [StringLength(15)]
        public String Phone { get; set; }
        // Employee Email, optional
        public String Email { get; set; }
        // Employee Adress, optional
        public String Address { get; set; }
        // Employee Hire Date
        [DataType(DataType.Date)]
        public DateTime HireDate { get; set; }
        // Employee Hire Date
        [DataType(DataType.Date)]
        public DateTime LastModified { get; set; }

        // Navigation Properties
        public IList<SkillAssignment> SkillAssignments { get; set; }
        public IList<AuditEvent> AuditEvents { get; set; }
    }
}
