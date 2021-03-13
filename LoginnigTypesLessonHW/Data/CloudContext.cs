using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Text;

namespace LoginnigTypesLessonHW.Data
{
    public class CloudContext : DbContext
    {
        public CloudContext()
        {
            Database.EnsureCreated();
        }

        public DbSet<CloudFile> Files { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server = DESKTOP-619RM10; DataBase = LoadingTypesLessonHW; Trusted_connection = true;");
        }
    }
}
