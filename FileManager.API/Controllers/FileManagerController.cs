using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileManagerAPI.Application.Repositories.MusteriRepository;

namespace FileManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileManagerController : ControllerBase
    {
        readonly private IMusteriReadRepository _musteriReadRepository;
       
        public FileManagerController(IMusteriReadRepository musteriReadRepository)
        {
            _musteriReadRepository = musteriReadRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
          return Ok ( _musteriReadRepository.GetAll(false));

        }
        [Route("GetMusterile")]
        [HttpGet]
        public async Task<IActionResult> GetMusterile()
        {
            return Ok(_musteriReadRepository.GetAll(false));

        }



    }
}
