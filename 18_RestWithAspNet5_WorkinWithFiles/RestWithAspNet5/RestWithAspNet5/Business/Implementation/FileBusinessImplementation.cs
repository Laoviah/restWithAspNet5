using Microsoft.AspNetCore.Http;
using RestWithAspNet5.Data.VO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RestWithAspNet5.Business.Implementation
{
    public class FileBusinessImplementation : IFileBusiness
    {
        private readonly string _basePath;
        private readonly IHttpContextAccessor _context;

        public FileBusinessImplementation(IHttpContextAccessor context)
        {
            _context = context;
            _basePath = Directory.GetCurrentDirectory() + "\\UploadDir\\";
        }

        public byte[] GetFile(string fileName)
        {
            var filePath = _basePath + fileName;

            return File.ReadAllBytes(filePath);
        }

        public async Task<FileDatailVO> SaveFileToDisk(IFormFile file)
        {
            FileDatailVO fileDetail = new FileDatailVO();

            var fileType = Path.GetExtension(file.FileName);
            var baseUrl = _context.HttpContext.Request.Host;

            if (fileType.ToLower() == ".pdf" || fileType.ToLower() == ".jpg" || 
                fileType.ToLower() == ".png" || fileType.ToLower() == ".jpeg")
            {
                var docName = Path.GetFileName(file.FileName);

                if (file != null && file.Length > 0)
                {
                    var destination = Path.Combine(_basePath, "", docName);
                    fileDetail.DocumentName = docName;
                    fileDetail.DocumentType = fileType;
                    fileDetail.DocumentUrl = Path.Combine(baseUrl + "/api/v1/file/" + fileDetail.DocumentName);

                    using var stream = new FileStream(destination, FileMode.Create);

                    await file.CopyToAsync(stream); 
                }
            }


            return fileDetail;
        }

        public async Task<List<FileDatailVO>> SaveFilesToDisk(IList<IFormFile> files)
        {
            List<FileDatailVO> list = new List<FileDatailVO>();

            foreach (var file in files)
            {
                list.Add(await SaveFileToDisk(file));   
            } 


            return list;
        }

    }
}
