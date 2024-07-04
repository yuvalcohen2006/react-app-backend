using Microsoft.EntityFrameworkCore;
using react_practice_backend.Models;
using System.Collections.Generic;

namespace practice_backend.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<react_practice_backend.Models.DBTask> Tasks { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    }
}
