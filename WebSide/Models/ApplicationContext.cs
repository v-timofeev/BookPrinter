using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebSide.Models
{
    public class ApplicationContext :DbContext
    {
        public DbSet<BookModel> Files { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) :base(options)
        {
            Database.EnsureCreated();
        }
    }
}
