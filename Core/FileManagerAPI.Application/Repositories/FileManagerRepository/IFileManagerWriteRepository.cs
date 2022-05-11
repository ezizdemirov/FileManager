using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileManagerAPI.Domain.Entities;

namespace FileManagerAPI.Application.Repositories.FileManagerRepository
{
   public interface IFileManagerWriteRepository : IWriteRepository<FileManager>
    {
    }
}
