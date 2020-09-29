using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Any;
using SchedulerAPI.Models;

namespace SchedulerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkillAssignmentsController : ControllerBase
    {
        private readonly SchedulerContext _context;

        public SkillAssignmentsController(SchedulerContext context)
        {
            _context = context;
        }

        // GET: api/Skills: Retrieve all Skills
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SkillAssignment>>> GetSkillAssignments()
        {
            return await _context.SkillAssignments.ToListAsync();
        }

        // GET: api/SkillAssignments/13: Get a Skill Assignment by Id
        [HttpGet("{id}")]
        public async Task<ActionResult<SkillAssignment>> GetSkillAssignments([FromRoute] long id)
        {
            return await _context.SkillAssignments.FindAsync(id);
        }

        // POST: api/SkillAssignments: Create a new Skill Assignment
        [HttpPost]
        public async Task<ActionResult<SkillAssignment>> PostSkillAssignments([FromBody] SkillAssignment skillAssignment)
        {
            var skillAssignments = _context.SkillAssignments
                .Where(sa => sa.SkillId == skillAssignment.SkillId
                    && sa.EmployeeId == skillAssignment.EmployeeId)
                .ToListAsync();

            if (skillAssignments.Result.Count() != 0)
            {
                return BadRequest();
            }

            skillAssignment.AssignmentDate = DateTime.UtcNow;
            _context.SkillAssignments.Add(skillAssignment);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (SkillAssignmentsExists(skillAssignment.EmployeeId, skillAssignment.SkillId))
                {
                    return BadRequest();
                }
                else
                {
                    return StatusCode(500);
                }
            }

            return CreatedAtAction(
                nameof(GetSkillAssignments),
                new { employeeId = skillAssignment.EmployeeId, skillId = skillAssignment.SkillId },
                skillAssignment
                );
        }

        // DELETE: api/SkillAssignments/5: Delete a single Skill Assignment
        [HttpDelete("{id}")]
        public async Task<ActionResult<SkillAssignment>> DeleteSkillAssignments([FromRoute] long id)
        {
            var skillAssignments = await _context.SkillAssignments.FindAsync(id);

            if (skillAssignments == null)
            {
                return NotFound();
            }

            _context.SkillAssignments.Remove(skillAssignments);
            await _context.SaveChangesAsync();

            return skillAssignments;
        }

        private bool SkillAssignmentsExists(int employeeId, int skillId)
        {
            return _context.SkillAssignments.Any(e => e.EmployeeId == employeeId && e.SkillId == skillId);
        }
    }
}
