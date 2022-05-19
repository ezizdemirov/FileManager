using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FileManagerAPI.Application.Repositories.FileManagerRepository;
using FileManagerAPI.Application.ViewModels;
using FileManager = FileManagerAPI.Domain.Entities.FileManager;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileManagerController : ControllerBase
    {
        
        readonly private IFileManagerReadRepository _fileManagerReadRepository;
        readonly private IFileManagerWriteRepository _fileManagerWriteRepository;

        public FileManagerController( IFileManagerReadRepository fileManagerReadRepository, IFileManagerWriteRepository fileManagerWriteRepository)
        {
           
            _fileManagerReadRepository = fileManagerReadRepository;
            _fileManagerWriteRepository = fileManagerWriteRepository;
        }

   
        [Route("GetFileManager")]
        [HttpGet]
        public IActionResult GetFileManager()
        {
            var data = _fileManagerReadRepository.GetAll(false).OrderBy(x => x.Id);
            return Ok(data);

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
        [Route("UploadFile")]
    //    public async Task<IActionResult> SavePost([FromForm] PostViewModel viewModel)
        public async Task<IActionResult> UploadFile([FromForm] UploadViewModel model)
        {
           
            try
            {
                var file = Request.Form.Files[0];
                var folderName = Path.Combine("Resources", "Files");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                if (file.Length > 0)
                {
                    var fileName = model.File.FileName;
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(folderName, fileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                    var data = await _fileManagerReadRepository.GetByIdAsync(model.Id, true);
                    data.Name = model.File.FileName;
                    data.Expanded = true;
                    data.IsDirectory = false;
                    data.Icon = "file";
                    data.Id = 0;
                    data.ParentId = model.Id;


                    
                   await  _fileManagerWriteRepository.AddAsync(data);
                  
                   // return Ok(new { dbPath });
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
            await _fileManagerWriteRepository.SaveAsync();
            return Ok();
            
        }
       [HttpGet]
       [Route("GetFiles")]
       public  IActionResult GetFiles(int id)
       {
           var files = _fileManagerReadRepository.GetWhere(x => x.ParentId == id && x.IsDirectory==false,false).ToList();
           return Ok(files);
       }

    }
}
