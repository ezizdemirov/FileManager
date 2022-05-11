using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileManagerAPI.Application.Repositories.FileManagerRepository;
using FileManagerAPI.Domain.Entities;
using FileManagerAPI.Persistence.Contexts;

namespace FileManagerAPI.Persistence.Repositories
{
   public class FileManagerReadRepository:ReadRepository<FileManager>, IFileManagerReadRepository
    {
        public FileManagerReadRepository(FileManagerAPIDbContext context) : base(context)
        {
            
        }
    }
}
