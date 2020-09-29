using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using SchedulerAPI.Models;

namespace SchedulerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly SchedulerContext _context;

        public EmployeesController(SchedulerContext context)
        {
            _context = context;
        }

        // GET: api/Employees: Get all Employees, ordered by Surname
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
        {
            return await _context.Employees
                .OrderBy(e => e.Surname)
                .ToListAsync();
        }

        // GET: api/Employees/1: Get all information about an Employee
        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployee([FromRoute] int id)
        {
            var employee = await _context.Employees
                .Where(e => e.EmployeeId == id)
                .Include(e => e.AuditEvents)
                .Include(e => e.SkillAssignments)
                .ThenInclude(sa => sa.Skill)
                .FirstAsync();

            if (employee == null)
            {
                return NotFound();
            }

            return employee;
        }

        // GET: api/Employees/search?by=name&q=John: Search for an Employee by name, surname or both
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployeeByName(
            [FromQuery] string by,
            [FromQuery] string q)
        {
            return by switch
            {
                "name" => await _context.Employees
                .Where(e => e.Name.Contains(q))
                .OrderBy(e => e.Name)
                .Include(e => e.SkillAssignments)
                .ThenInclude(sa => sa.Skill)
                .ToListAsync(),
                "surname" => await _context.Employees
                .Where(e => e.Surname.Contains(q))
                .OrderBy(e => e.Surname)
                .Include(e => e.SkillAssignments)
                .ThenInclude(sa => sa.Skill)
                .ToListAsync(),
                "any" => await _context.Employees
                .Where(e => e.Name.Contains(q) || e.Surname.Contains(q))
                .OrderBy(e => e.Surname)
                .Include(e => e.SkillAssignments)
                .ThenInclude(sa => sa.Skill)
                .ToListAsync(),
                _ => BadRequest(),
            };
        }

        // PUT: api/Employees/1: Update information on an Employee
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(
            [FromRoute] int id,
            [FromBody] Employee employee)
        {
            if (id != employee.EmployeeId)
            {
                return BadRequest();
            }

            _context.Entry(employee).State = EntityState.Modified;
            employee.LastModified = DateTime.UtcNow;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
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

        // POST: api/Employees: Create a new Employee
        [HttpPost]
        public async Task<ActionResult<Employee>> PostEmployee([FromBody] Employee employee)
        {
            if (String.IsNullOrEmpty(employee.Name)
                || String.IsNullOrEmpty(employee.Surname)
                || EmployeeEmailExists(employee.Email))
            {
                return BadRequest();
            }

            employee.HireDate = DateTime.UtcNow;
            employee.LastModified = DateTime.UtcNow;
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetEmployee),
                new { id = employee.EmployeeId },
                employee
                );
        }

        // DELETE: api/Employees/7: Delete an Employee by their Id
        [HttpDelete("{id}")]
        public async Task<ActionResult<Employee>> DeleteEmployee([FromRoute] int id)
        {
            var employee = await _context.Employees.FindAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();

            return employee;
        }

        // DELETE: api/Employees?id=7&id=2&id=11: Delete multiple Employees by their Ids
        [HttpDelete]
        public async Task<ActionResult<IEnumerable<Employee>>> DeleteEmployees([FromQuery] int[] id)
        {
            var employees = await _context.Employees
                .Where(e => id.Contains(e.EmployeeId))
                .ToListAsync();

            if (employees == null)
            {
                return NotFound();
            }

            _context.Employees.RemoveRange(employees);
            await _context.SaveChangesAsync();

            return employees;
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.EmployeeId == id);
        }

        private bool EmployeeEmailExists(string email)
        {
            return _context.Employees.Any(e => e.Email == email);
        }
    }
}
