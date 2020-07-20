using Microsoft.EntityFrameworkCore;
using Pseudo_MD_DoC_API.Applications;
using Pseudo_MD_DoC_API.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pseudo_MD_DoC_API.Persistence
{
    public class AppDbContext : DbContext
    {
        public DbSet<Application> Applications { get; set; }
        public DbSet<User> Users { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

    }
}
