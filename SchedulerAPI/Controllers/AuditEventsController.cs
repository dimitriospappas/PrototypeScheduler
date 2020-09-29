using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchedulerAPI.Models;

namespace SchedulerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuditEventsController : ControllerBase
    {
        private readonly SchedulerContext _context;

        public AuditEventsController(SchedulerContext context)
        {
            _context = context;
        }

        // GET: api/AuditEvents: Retrieve all Audit Events
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuditEvent>>> GetAuditEvents()
        {
            return await _context.AuditEvents.ToListAsync();
        }

        // GET: api/AuditEvents/3: Retrieve Audit Events for an Employee by their Id
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<AuditEvent>>> GetAuditEventForEmployee([FromRoute] int id)
        {
            var auditEvent = await _context.AuditEvents
                .Where(ae => ae.EmployeeId == id)
                .OrderByDescending(ae => ae.EventDate)
                .ToListAsync();

            if (auditEvent == null)
            {
                return NotFound();
            }

            return auditEvent;
        }
    }
}
