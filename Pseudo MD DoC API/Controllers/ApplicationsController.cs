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
using Pseudo_MD_DoC_API.Services;
using Microsoft.AspNetCore.Authentication;
//using Microsoft.Extensions.Configuration;

namespace Pseudo_MD_DoC_API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IApplicationService _applicationService;
        private readonly IUserService _userService;

        public ApplicationsController(AppDbContext context, IMapper mapper,IApplicationService applicationService, IUserService userService)
        {
            _context = context;
            _mapper = mapper;
            _applicationService = applicationService;
            _userService = userService;
        }

        private bool getAdminStatus()
        {
            return _userService.isAdmin(int.Parse(User.Identity.Name));
        }

        // GET: api/Applications
        [HttpGet]
        public async Task<IActionResult> GetApplications()
        {
            try
            {
                var applications = await _applicationService.GetAll();
                return Ok(applications);
            }
            catch(Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // GET: api/Applications/user/1
        [HttpGet("user/{id}")]
        public async Task<IActionResult> GetUserApplications(int id)
        {
            try
            {
                var user = await _context.Users.SingleAsync(u => u.Id == id);
                var applications = await _applicationService.GetAll(user);
                return Ok(applications);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // GET: api/Applications/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ApplicationOutputModel>> GetApplication(int id)
        {
            try
            {
                var application = await _applicationService.GetById(id);
                return Ok(application);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("status")]
        public async Task<IActionResult> UpdateApplicationStatus([FromBody] ApplicationStatusModel applicationStatus)
        {
            try
            {
                var application = await _applicationService.UpdateStatus(applicationStatus);
                return Ok(application);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // PUT: api/Applications/5
        //TODO: Move to application service
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutApplication(int id, Applications.Application application)
        {
            throw new NotImplementedException();
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
        [HttpPost]
        public async Task<IActionResult> PostApplication([FromBody] ApplicationSaveModel application)
        {
            try
            {
                var newApp = await _applicationService.Create(application);
                return CreatedAtAction("GetApplication", new { id = newApp.Id }, newApp);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // DELETE: api/Applications/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Applications.Application>> DeleteApplication(int id)
        {
            try
            {
                await _applicationService.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        private bool ApplicationExists(int id)
        {
            return _context.Applications.Any(e => e.Id == id);
        }
    }
}
