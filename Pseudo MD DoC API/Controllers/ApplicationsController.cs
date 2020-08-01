using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pseudo_MD_DoC_API.Applications;
using Pseudo_MD_DoC_API.Persistence;
using AutoMapper;
using Pseudo_MD_DoC_API.Models.Applications;

namespace Pseudo_MD_DoC_API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public ApplicationsController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Applications
        [HttpGet]
        public async Task<IEnumerable<ApplicationModel>> GetApplications()
        {
            var applications = await _context.Applications
                .Include(a => a.Education)
                .Include(a => a.References)
                .Include(a => a.Employment)
                .Include(a => a.User)
                .ToListAsync();

            //map to output model
            IEnumerable<ApplicationModel> newApplications = _mapper.Map<IEnumerable<ApplicationModel>>(applications);

            return newApplications;
        }

        // GET: api/Applications/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ApplicationModel>> GetApplication(int id)
        {
            var application = await _context.Applications
                .Include(a => a.Education)
                .Include(a => a.References)
                .Include(a => a.Employment)
                .Include(a => a.User)
                .FirstOrDefaultAsync(i => i.Id == id);

            //not found
            if (application == null)
                return NotFound();

            //map to output model
            ApplicationModel newApplication = _mapper.Map<ApplicationModel>(application);

            return newApplication;
        }

        // PUT: api/Applications/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutApplication(int id, Application application)
        {
            if (id != application.Id)
            {
                return BadRequest();
            }

            _context.Entry(application).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ApplicationExists(id))
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

        // POST: api/Applications
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Application>> PostApplication(Application application)
        {
            //SET THE RECEIVED DATE TO NOW
            application.DateReceived = DateTime.Now;

            //ADD
            _context.Applications.Add(application);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetApplication", new { id = application.Id }, application);
        }

        // DELETE: api/Applications/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Application>> DeleteApplication(int id)
        {
            var application = await _context.Applications.FindAsync(id);
            if (application == null)
            {
                return NotFound();
            }

            _context.Applications.Remove(application);
            await _context.SaveChangesAsync();

            return application;
        }

        private bool ApplicationExists(int id)
        {
            return _context.Applications.Any(e => e.Id == id);
        }
    }
}
