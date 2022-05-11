using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileManagerAPI.Application.Repositories.FileManagerRepository;
using FileManagerAPI.Application.Repositories.MusteriRepository;

namespace FileManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileManagerController : ControllerBase
    {
        readonly private IMusteriReadRepository _musteriReadRepository;
        readonly private IFileManagerReadRepository _fileManagerReadRepository;
       
        public FileManagerController(IMusteriReadRepository musteriReadRepository, IFileManagerReadRepository fileManagerReadRepository)
        {
            _musteriReadRepository = musteriReadRepository;
            _fileManagerReadRepository = fileManagerReadRepository;

        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
          return Ok ( _musteriReadRepository.GetAll(false));

        }
        [Route("GetFileManager")]
        [HttpGet]
        public async Task<IActionResult> GetFileManager()
        {
            return Ok(_fileManagerReadRepository.GetAll(false).OrderBy(x=>x.Id));

        }

        [Route("CreateFolder")]
        [HttpPost]
        public async Task<IActionResult> CreateFolder()
        {
           // return Ok(_fileManagerReadRepository.GetAll(false).OrderBy(x => x.Id));
           return Ok();

        }




    }
}
