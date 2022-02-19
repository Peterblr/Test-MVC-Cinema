using Cinema.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cinema.Data
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }

        public virtual DbSet<Actor> Actors { get; set; }
        public virtual DbSet<Producer> Producers { get; set; }
        public virtual DbSet<Movie> Movies { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Grade> Grades { get; set; }
    }
}
