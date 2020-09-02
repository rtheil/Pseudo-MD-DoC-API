using AutoMapper;
using Pseudo_MD_DoC_API.Applications;
using Pseudo_MD_DoC_API.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pseudo_MD_DoC_API.Models.Applications;
using Microsoft.EntityFrameworkCore;
using Pseudo_MD_DoC_API.Users;

namespace Pseudo_MD_DoC_API.Services
{
    public interface IApplicationService
    {
        public Task<IEnumerable<ApplicationOutputModel>> GetAll();
        public Task<IEnumerable<ApplicationOutputModel>> GetAll(User user);
        public Task<ApplicationOutputModel> GetById(int id);
        public Task<ApplicationOutputModel> Create(ApplicationSaveModel application);
        public Task<ApplicationOutputModel> Update(ApplicationSaveModel application);
        public Task Delete(int id);
        public Task<ApplicationOutputModel> UpdateStatus(ApplicationStatusModel application);
    }
    public class ApplicationService : IApplicationService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public ApplicationService(AppDbContext context, IMapper mapper, IUserService userService)
        {
            _context = context;
            _mapper = mapper;
            _userService = userService;
        }
        private async Task<bool> hasPermission(int userId, Application application)
        {
            var user = await _context.Users.FirstAsync(u => u.Id == userId);
            if (user == application.User || user.Administrator)
                return true;
            throw new Exception("Permission denied");
        }

        public async Task<IEnumerable<ApplicationOutputModel>> GetAll(User user)
        {
            //var user = await _context.Users.SingleAsync(u => u.Id == userId);
            var applications = await _context.Applications
                .Include(a => a.Education)
                .Include(a => a.References)
                .Include(a => a.Employment)
                .Include(a => a.User)
                .Include(a => a.ApplicationStatus)
                .Where(a => a.User == user)
                .ToListAsync();

            //map to output model
            IEnumerable<ApplicationOutputModel> newApplications = _mapper.Map<IEnumerable<ApplicationOutputModel>>(applications);
            return newApplications;
        }


        public async Task<IEnumerable<ApplicationOutputModel>> GetAll()
        {
            var applications = await _context.Applications
                .Include(a => a.Education)
                .Include(a => a.References)
                .Include(a => a.Employment)
                .Include(a => a.User)
                .Include(a => a.ApplicationStatus)
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
                .Include(a => a.ApplicationStatus)
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
            var user = await _context.Users.SingleAsync(u => u.Id == application.UserId);
            saveApplication.User = user;

            //find application status
            var appStatus = await _context.ApplicationStatus.SingleAsync(a=> a.Id==application.ApplicationStatusId);
            saveApplication.ApplicationStatus = appStatus;

            //ADD
            _context.Applications.Add(saveApplication);
            await _context.SaveChangesAsync();

            return _mapper.Map<ApplicationOutputModel>(saveApplication);
        }

        public async Task<ApplicationOutputModel> Update(ApplicationSaveModel application)
        {
            return new ApplicationOutputModel();//TESTING ONLY
        }

        public async Task<ApplicationOutputModel> UpdateStatus(ApplicationStatusModel status)
        {
            var application = await _context.Applications
                .Include(a => a.Education)
                .Include(a => a.References)
                .Include(a => a.Employment)
                .Include(a => a.User)
                .Include(a => a.ApplicationStatus)
                .SingleAsync(a => a.Id == status.Id);
            
            if (application == null)
                throw new Exception("Not Found");

            //status change logic
            if (status.statusAction== "backgroundCheckSubmitted")
            {
                //status should be 1, update status to 2 (background check pending)
                application.ApplicationStatus = await _context.ApplicationStatus.SingleAsync(s => s.Id == 2);
            }
            else if (status.statusAction== "backgroundCheckPassed")
            {
                //status should be 2, update status to 3 (test pending)
                application.ApplicationStatus = await _context.ApplicationStatus.SingleAsync(s => s.Id == 3);
                application.DateBackgroundCheckComplete = DateTime.Now;
            }
            else if (status.statusAction == "backgroundCheckFailed")
            {
                //status should be 2, update status to 6 (rejected)
                application.ApplicationStatus = await _context.ApplicationStatus.SingleAsync(s => s.Id == 6);
                application.DateBackgroundCheckComplete = DateTime.Now;
            }
            else if (status.statusAction == "submitTestScore")
            {
                //status should be 4
                application.TestScore = status.TestScore;
                
                //check valid test score value
                if (status.TestScore > 100 || status.TestScore < 0 || status.TestScore==null)
                    throw new Exception("Invalid Test Score");
                //if status.testScore>=60, update status to 5 (offer)
                else if (status.TestScore>=60)
                    application.ApplicationStatus = await _context.ApplicationStatus.SingleAsync(s => s.Id == 5);
                //is testScore<60, update status to 4 (failed test)
                else if (status.TestScore<60)
                    application.ApplicationStatus = await _context.ApplicationStatus.SingleAsync(s => s.Id == 4);
            }

            //update and save
            _context.Applications.Update(application);
            await _context.SaveChangesAsync();

            return _mapper.Map<ApplicationOutputModel>(application);
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
