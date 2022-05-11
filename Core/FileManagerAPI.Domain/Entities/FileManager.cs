using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileManagerAPI.Domain.Entities.Common;

namespace FileManagerAPI.Domain.Entities
{
   public class FileManager:BaseEntity
    {
        
        public int? ParentId { get; set; }
        public string Icon { get; set; }
        public bool IsDirectory { get; set; }
        public bool? Expanded { get; set; }
        public string Name { get; set; }
    }
}


