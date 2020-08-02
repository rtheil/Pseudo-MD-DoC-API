using AutoMapper;
using Pseudo_MD_DoC_API.Applications;
using Pseudo_MD_DoC_API.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pseudo_MD_DoC_API.Models.Applications;
using Microsoft.EntityFrameworkCore;

namespace Pseudo_MD_DoC_API.Services
{
    public interface IApplicationService
    {
        public Task<IEnumerable<ApplicationOutputModel>> GetAll();
        public Task<ApplicationOutputModel> GetById(int id);
        public Task<ApplicationOutputModel> Create(ApplicationSaveModel application);
        public Task<ApplicationOutputModel> Update(ApplicationSaveModel application);
        public Task Delete(int id);
    }
    public class ApplicationService : IApplicationService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public ApplicationService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ApplicationOutputModel>> GetAll()
        {
            var applications = await _context.Applications
                .Include(a => a.Education)
                .Include(a => a.References)
                .Include(a => a.Employment)
                .Include(a => a.User)
                .ToListAsync();

            //map to output model
            IEnumerable<ApplicationOutputModel> newApplications = _mapper.Map<IEnumerable<ApplicationOutputModel>>(applications);
            return newApplications;

        }

        public async Task<ApplicationOutputModel> GetById(int id)
        {
            var application = await _context.Applications
                .Include(a => a.Education)
                .Include(a => a.References)
                .Include(a => a.Employment)
                .Include(a => a.User)
                .FirstOrDefaultAsync(i => i.Id == id);

            //not found
            if (application == null)
                throw new Exception("Not Found");

            //map to output model
            ApplicationOutputModel newApplication = _mapper.Map<ApplicationOutputModel>(application);

            return newApplication;

        }

        public async Task<ApplicationOutputModel> Create(ApplicationSaveModel application)
        {
            //SET THE RECEIVED DATE TO NOW
            application.DateReceived = DateTime.Now;

            //map
            var saveApplication = _mapper.Map<Application>(application);

            //find user
            var user = _context.Users.SingleOrDefault(u => u.Id == application.UserId);
            saveApplication.User = user;

            //ADD
            _context.Applications.Add(saveApplication);
            await _context.SaveChangesAsync();

            return _mapper.Map<ApplicationOutputModel>(saveApplication);
        }

        public async Task<ApplicationOutputModel> Update(ApplicationSaveModel application)
        {
            return new ApplicationOutputModel();//TESTING ONLY
        }

        public async Task Delete(int id)
        {
            var application = await _context.Applications
                .Include(a => a.Education)
                .Include(a => a.References)
                .Include(a => a.Employment)
                .SingleAsync(a => a.Id == id);

            if (application == null)
                throw new Exception("Not Found");

            _context.Applications.Remove(application);
            await _context.SaveChangesAsync();
        }
    }
}
