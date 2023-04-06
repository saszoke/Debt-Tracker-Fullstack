using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using webapi.Models;

namespace webapi.Data
{
    public class webapiContext : IdentityDbContext
    {
        public webapiContext (DbContextOptions<webapiContext> options)
            : base(options)
        {
        }

        public DbSet<webapi.Models.Transaction> Transaction { get; set; } = default!;
        public DbSet<webapi.Models.Account> Account { get; set; } = default!;

    }
}
