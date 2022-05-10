using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileManagerAPI.Application.Repositories.MusteriRepository;
using FileManagerAPI.Domain.Entities;
using FileManagerAPI.Persistence.Contexts;


namespace FileManagerAPI.Persistence.Repositories
{
   public class MusteriReadRepository :  ReadRepository<Musteri>, IMusteriReadRepository
    {
        public MusteriReadRepository(FileManagerAPIDbContext context) : base(context)
        {
        }
    }
}
