﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestWithAspNet5.Business;
using RestWithAspNet5.Data.VO;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace RestWithAspNet5.Controllers
{
    [ApiController]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class FileController : Controller
    {
        private readonly IFileBusiness _fileBusiness;

        public FileController(IFileBusiness fileBusiness)
        {
            _fileBusiness = fileBusiness;
        }

        [HttpPost("uploadFile")]
        [ProducesResponseType((200), Type = typeof(FileDatailVO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [Produces("application/json")]

        public async Task<IActionResult> UploadOneFile([FromForm] IFormFile file)
        {
            FileDatailVO detail = await _fileBusiness.SaveFileToDisk(file);

            return new OkObjectResult(detail);
        }
        
        
        [HttpPost("uploadMultipleFile")]
        [ProducesResponseType((200), Type = typeof(List<FileDatailVO>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [Produces("application/json")]

        public async Task<IActionResult> UploadMultipleFile([FromForm] List<IFormFile> files)
        {
            List<FileDatailVO> details = await _fileBusiness.SaveFilesToDisk(files);

            return new OkObjectResult(details);
        }
        
        [HttpGet("downloadFile/{fileName}")]
        [ProducesResponseType((200), Type = typeof(byte[]))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [Produces("application/octet-stream")]

        public async Task<IActionResult> GetFileAsync(string fileName)
        {
            byte[] buffer = _fileBusiness.GetFile(fileName);

            if (buffer != null)
            {
                HttpContext.Response.ContentType = $"application/{Path.GetExtension(fileName).Replace(".", "")}";
                HttpContext.Response.Headers.Add("content-length", buffer.Length.ToString());

                await HttpContext.Response.Body.WriteAsync(buffer, 0, buffer.Length);

            }

            return new ContentResult();
        }
    }
}
