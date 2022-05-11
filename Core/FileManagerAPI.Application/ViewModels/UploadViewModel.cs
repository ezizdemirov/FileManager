using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace FileManagerAPI.Application.ViewModels
{
  public class UploadViewModel
    {
        public int Id { get; set; }
        public IFormFile File { get; set; }
    }
}
