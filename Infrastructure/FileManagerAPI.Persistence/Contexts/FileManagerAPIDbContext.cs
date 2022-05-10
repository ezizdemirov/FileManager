using FileManagerAPI.Domain.Entities;
using FileManagerAPI.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FileManagerAPI.Persistence.Contexts
{
    public class FileManagerAPIDbContext :  DbContext
    {
        public FileManagerAPIDbContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<FileManager> FileManager { get; set; }
        public DbSet<Musteri> Musteri { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
             var datas = ChangeTracker
                .Entries<BaseEntity>();

            foreach (var data in datas)
            {
                _ = data.State switch
                {
                    EntityState.Added => data.Entity.CreatedDate = DateTime.UtcNow,
                    
                    _ => DateTime.UtcNow
                };
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
