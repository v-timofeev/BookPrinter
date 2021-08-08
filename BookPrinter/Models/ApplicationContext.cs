﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BookPrinter.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<BookModel> Books { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        { 
            Database.EnsureCreated();
        }
    }
}
