using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileManagerAPI.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace FileManagerAPI.Persistence
{
 

    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<FileManagerAPIDbContext>
    {
        public FileManagerAPIDbContext CreateDbContext(string[] args)
        {
            DbContextOptionsBuilder<FileManagerAPIDbContext> dbContextOptionsBuilder = new DbContextOptionsBuilder<FileManagerAPIDbContext>();
            dbContextOptionsBuilder.UseNpgsql(Configuration.ConnectionString);
            return new FileManagerAPIDbContext(dbContextOptionsBuilder.Options); 
        }
    }
}
