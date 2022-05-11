using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FileManagerAPI.Application.Repositories.FileManagerRepository;
using FileManagerAPI.Application.Repositories.MusteriRepository;
using FileManager = FileManagerAPI.Domain.Entities.FileManager;

namespace FileManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileManagerController : ControllerBase
    {
        readonly private IMusteriReadRepository _musteriReadRepository;
        readonly private IFileManagerReadRepository _fileManagerReadRepository;
        readonly private IFileManagerWriteRepository _fileManagerWriteRepository;

        public FileManagerController(IMusteriReadRepository musteriReadRepository, IFileManagerReadRepository fileManagerReadRepository, IFileManagerWriteRepository fileManagerWriteRepository)
        {
            _musteriReadRepository = musteriReadRepository;
            _fileManagerReadRepository = fileManagerReadRepository;
            _fileManagerWriteRepository = fileManagerWriteRepository;
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
        public async Task<IActionResult> CreateFolder([FromBody] FileManagerAPI.Domain.Entities.FileManager model)
        {
            if (model.Id>0)
            { 
                var data =  await _fileManagerReadRepository.GetByIdAsync(model.Id, true);
                data.Name = model.Name;
                _fileManagerWriteRepository.Update(data);
                await _fileManagerWriteRepository.SaveAsync();
               
            }
            else
            {
                FileManagerAPI.Domain.Entities.FileManager folder =  new FileManagerAPI.Domain.Entities.FileManager();
                folder.Icon = model.Icon;
                folder.Name = model.Name;
                folder.ParentId = model.ParentId;
                folder.Expanded = model.Expanded;
                folder.IsDirectory = model.IsDirectory;
                folder.CreatedDate = model.CreatedDate;
                await _fileManagerWriteRepository.AddAsync(folder);
                await _fileManagerWriteRepository.SaveAsync();
            }

            return Ok();


        }

        [Route("DeleteFolder")]
        [HttpDelete]
        public async Task<IActionResult> DeleteFolder(int id)
        {
            var folder = _fileManagerReadRepository.GetWhere(x => x.Id == id).FirstOrDefault();
            _fileManagerWriteRepository.Remove(folder);
            var childNodes = _fileManagerReadRepository.GetWhere(x => x.ParentId == id).ToList();
            _fileManagerWriteRepository.RemoveRange(childNodes);


            await _fileManagerWriteRepository.SaveAsync();

            return Ok();
        }
        
        [HttpGet]
        [Route("GetById")]
        public async Task<IActionResult> GetById(int id)
        {
            var model = await _fileManagerReadRepository.GetByIdAsync(id, false);
            return Ok(model);

        }

        [HttpPost]
        public async Task<IActionResult> Post(IFormFile file)
        {
            if (file.Length <= 0)
                return BadRequest("Empty file");

           var originalFileName = Path.GetFileName(file.FileName);

            //Create a unique file path
            var uniqueFileName = Path.GetRandomFileName();
            var uniqueFilePath = Path.Combine(@"C:\temp\", uniqueFileName);

            //Save the file to disk
            using (var stream = System.IO.File.Create(uniqueFilePath))
            {
                await file.CopyToAsync(stream);
            }

            return Ok($"Saved file {originalFileName} with size {file.Length / 1024m:#.00} KB using unique name {uniqueFileName}");
        }
    }
}
