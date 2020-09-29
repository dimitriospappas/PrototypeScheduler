using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Net.Mime;

namespace SchedulerAPI.Models
{
    // Class to allow seeding the DB (should use migrations)
    public class InitialSeed
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new SchedulerContext(serviceProvider
                .GetRequiredService<DbContextOptions<SchedulerContext>>()))
            {
                // Seed Employees, don't run if there are some
                if (!context.Employees.Any())
                {
                    context.Employees.AddRange(
                        new Employee
                        {
                            Name = "John D.",
                            Surname = "Carmack",
                            HireDate = new DateTime(2008, 5, 1, 8, 30, 52),
                            Address = "New York",
                            Email = "jc@company.com",
                            Phone = "12376415",
                            LastModified = new DateTime(2009, 10, 10, 8, 30, 52)
                        },
                        new Employee
                        {
                            Name = "Brenda",
                            Surname = "Romero",
                            HireDate = new DateTime(2010, 1, 4, 10, 30, 2),
                            Address = "California",
                            Email = "br@company.com",
                            Phone = "6566234",
                            LastModified = new DateTime(2015, 9, 1, 5, 2, 4)
                        },
                        new Employee
                        {
                            Name = "Todd",
                            Surname = "Howard",
                            HireDate = new DateTime(2011, 6, 2, 18, 1, 40),
                            Address = "San Fransisco",
                            Email = "th@company.com",
                            Phone = "325326546t",
                            LastModified = new DateTime(2013, 11, 9, 1, 30, 1)
                        },
                        new Employee
                        {
                            Name = "Feargus",
                            Surname = "Urquhart",
                            HireDate = new DateTime(2015, 2, 1, 7, 11, 48),
                            Address = "GR",
                            Email = "fur@company.com",
                            Phone = "23452362",
                            LastModified = new DateTime(2019, 6, 9, 6, 30, 37)
                        },
                        new Employee
                        {
                            Name = "Ken",
                            Surname = "Levine",
                            HireDate = new DateTime(2005, 11, 12, 12, 3, 15),
                            Address = "London",
                            Email = "kl@company.com",
                            Phone = "21351324",
                            LastModified = new DateTime(2008, 5, 1, 12, 22, 41)
                        },
                        new Employee
                        {
                            Name = "Swen",
                            Surname = "Vincke",
                            HireDate = new DateTime(2013, 5, 5, 11, 25, 1),
                            Address = "China",
                            Email = "sv@company.com",
                            Phone = "091823921",
                            LastModified = new DateTime(2017, 2, 9, 17, 0, 0)
                        },
                        new Employee
                        {
                            Name = "Josh",
                            Surname = "Sawyer",
                            HireDate = new DateTime(2013, 1, 12, 18, 6, 11),
                            Address = "Neverland",
                            Email = "js@company.com",
                            Phone = "13251234213",
                            LastModified = new DateTime(2013, 12, 15, 9, 3, 22)
                        },
                        new Employee
                        {
                            Name = "Roberta",
                            Surname = "Williams",
                            HireDate = new DateTime(2004, 1, 6, 11, 20, 4),
                            Address = "Paris",
                            Email = "rw@company.com",
                            Phone = "324235115",
                            LastModified = new DateTime(2005, 11, 1, 8, 3, 7)
                        }
                    );
                    context.SaveChanges();
                }

                // Seed Skills, don't run if there are some
                if (!context.Skills.Any())
                {
                    context.Skills.AddRange(
                        new Skill
                        {
                            Name = "Manager",
                            Description = "Manages the establishment, financial executive officer",
                            CreationDate = new DateTime(2005, 11, 1, 8, 3, 7),
                            LastModified = new DateTime(2011, 5, 1, 3, 3, 23)
                        },
                        new Skill
                        {
                            Name = "Chef de cuisine",
                            Description = "Responsible for handling menu development, executive chef",
                            CreationDate = new DateTime(2005, 11, 1, 8, 3, 7),
                            LastModified = new DateTime(2006, 11, 6, 1, 5, 7)
                        },
                        new Skill
                        {
                            Name = "Sous-chef",
                            Description = "Capable of taking over as chef if needed, multiple posts",
                            CreationDate = new DateTime(2005, 10, 12, 5, 16, 17),
                            LastModified = new DateTime(2007, 3, 10, 15, 17, 9)
                        },
                        new Skill
                        {
                            Name = "Chef de partie",
                            Description = "Responsible for only one section in the kitchen",
                            CreationDate = new DateTime(2005, 2, 2, 3, 43, 37),
                            LastModified = new DateTime(2008, 2, 11, 13, 21, 26)
                        },
                        new Skill
                        {
                            Name = "Commis",
                            Description = "Chef in training assigned to specific smaller posts",
                            CreationDate = new DateTime(2005, 1, 7, 1, 23, 37),
                            LastModified = new DateTime(2006, 5, 6, 9, 15, 17)
                        },
                        new Skill
                        {
                            Name = "Security",
                            Description = "Security personel involved in protecting the establishment",
                            CreationDate = new DateTime(2004, 12, 5, 8, 31, 27),
                            LastModified = new DateTime(2005, 9, 2, 12, 13, 53)
                        },
                        new Skill
                        {
                            Name = "Cleaners",
                            Description = "Staff involved in cleaning operations and dishwashing",
                            CreationDate = new DateTime(2005, 3, 19, 19, 3, 1),
                            LastModified = new DateTime(2019, 1, 5, 13, 5, 27)
                        }
                    );
                    context.SaveChanges();
                }

                // Seed Skill Assignments, don't run if there are some
                if (!context.SkillAssignments.Any())
                {
                    context.SkillAssignments.AddRange(
                        new SkillAssignment
                        {
                            EmployeeId = 1,
                            SkillId = 1,
                            AssignmentDate = new DateTime(2016, 10, 12, 5, 16, 17)
                        },
                        new SkillAssignment
                        {
                            EmployeeId = 1,
                            SkillId = 2,
                            AssignmentDate = new DateTime(2016, 7, 1, 5, 16, 2)
                        },
                        new SkillAssignment
                        {
                            EmployeeId = 1,
                            SkillId = 5,
                            AssignmentDate = new DateTime(2016, 7, 5, 5, 25, 1)
                        },
                        new SkillAssignment
                        {
                            EmployeeId = 2,
                            SkillId = 1,
                            AssignmentDate = new DateTime(2016, 6, 7, 6, 22, 22)
                        },
                        new SkillAssignment
                        {
                            EmployeeId = 2,
                            SkillId = 3,
                            AssignmentDate = new DateTime(2016, 5, 21, 7, 12, 23)
                        },
                        new SkillAssignment
                        {
                            EmployeeId = 2,
                            SkillId = 7,
                            AssignmentDate = new DateTime(2016, 4, 23, 8, 3, 42)
                        },
                        new SkillAssignment
                        {
                            EmployeeId = 3,
                            SkillId = 3,
                            AssignmentDate = new DateTime(2016, 3, 17, 4, 32, 41)
                        },
                        new SkillAssignment
                        {
                            EmployeeId = 4,
                            SkillId = 4,
                            AssignmentDate = new DateTime(2016, 2, 12, 9, 55, 35)
                        },
                        new SkillAssignment
                        {
                            EmployeeId = 4,
                            SkillId = 5,
                            AssignmentDate = new DateTime(2016, 1, 12, 21, 1, 3)
                        },
                        new SkillAssignment
                        {
                            EmployeeId = 5,
                            SkillId = 6,
                            AssignmentDate = new DateTime(2016, 10, 12, 20, 2, 25)
                        },
                        new SkillAssignment
                        {
                            EmployeeId = 6,
                            SkillId = 6,
                            AssignmentDate = new DateTime(2016, 12, 12, 10, 53, 17)
                        },
                        new SkillAssignment
                        {
                            EmployeeId = 7,
                            SkillId = 2,
                            AssignmentDate = new DateTime(2016, 6, 12, 22, 23, 17)
                        }, new SkillAssignment
                        {
                            EmployeeId = 8,
                            SkillId = 5,
                            AssignmentDate = new DateTime(2016, 6, 12, 22, 23, 17)
                        }

                    );
                    context.SaveChanges();
                }
            }
        }
    }
}
