using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchedulerAPI.Models;

namespace SchedulerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkillsController : ControllerBase
    {
        private readonly SchedulerContext _context;

        public SkillsController(SchedulerContext context)
        {
            _context = context;
        }

        // GET: api/Skills: Retrieve all Skills
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Skill>>> GetSkills()
        {
            return await _context.Skills.ToListAsync();
        }

        // GET: api/Skills/3: Retrieve all details about a Skill
        [HttpGet("{id}")]
        public async Task<ActionResult<Skill>> GetSkill([FromRoute] int id)
        {
            var skill = await _context.Skills
                .Where(s => s.SkillId == id)
                .Include(s => s.SkillAssignments)
                .ThenInclude(sa => sa.Employee)
                .OrderBy(s => s.Name)
                .FirstAsync();

            if (skill == null)
            {
                return NotFound();
            }

            return skill;
        }

        // PUT: api/Skills/5: Update a Skill by its Id
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSkill(
            [FromRoute] int id,
            [FromBody] Skill skill)
        {
            if (id != skill.SkillId)
            {
                return BadRequest();
            }

            _context.Entry(skill).State = EntityState.Modified;
            skill.LastModified = DateTime.UtcNow;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SkillExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Skills: Create a new Skill
        [HttpPost]
        public async Task<ActionResult<Skill>> PostSkill([FromBody] Skill skill)
        {
            if (skill.Name == null)
            {
                return BadRequest();
            }

            var skills = await _context.Skills
                .Where(s => s.Name == skill.Name)
                .ToListAsync();

            if (skills.Count() != 0)
            {
                return BadRequest();
            }

            skill.CreationDate = DateTime.UtcNow;
            _context.Skills.Add(skill);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSkill), new { id = skill.SkillId }, skill);
        }

        // DELETE: api/Skills/7: Delete a Skill by its Id
        [HttpDelete("{id}")]
        public async Task<ActionResult<Skill>> DeleteSkill([FromRoute] int id)
        {
            var skill = await _context.Skills.FindAsync(id);
            if (skill == null)
            {
                return NotFound();
            }

            _context.Skills.Remove(skill);
            await _context.SaveChangesAsync();

            return skill;
        }

        private bool SkillExists(int id)
        {
            return _context.Skills.Any(e => e.SkillId == id);
        }
    }
}
