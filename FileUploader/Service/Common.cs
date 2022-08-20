using FileUploader.Data;
using FileUploader.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FileUploader.Services
{
    public class Common : ICommon
    {
        private readonly IWebHostEnvironment _iHostingEnvironment;
        private readonly ApplicationDbContext _context;
        public Common(IWebHostEnvironment iHostingEnvironment, ApplicationDbContext context)
        {
            _iHostingEnvironment = iHostingEnvironment;
            _context = context;
        }
        public string UploadedFile(IFormFile ProfilePicture)
        {
            string ProfilePictureFileName = null;
            if (ProfilePicture != null)
            {
                string uploadsFolder = Path.Combine(_iHostingEnvironment.ContentRootPath, "wwwroot\\upload");

                if (ProfilePicture.FileName == null)
                    ProfilePictureFileName = Guid.NewGuid().ToString() + "_" + "blank-person.png";
                else
                    ProfilePictureFileName = Guid.NewGuid().ToString() + "_" + ProfilePicture.FileName;
                string filePath = Path.Combine(uploadsFolder, ProfilePictureFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    ProfilePicture.CopyTo(fileStream);
                }
                return ProfilePictureFileName;
            }
            return "blank-person.png";
        }

        public async Task<AttachmentFile> AddAttachmentFile(IFormFile _IFormFile)
        {
            try
            {
                string _FileName = UploadedFile(_IFormFile);
                AttachmentFile _AttachmentFile = new AttachmentFile
                {
                    FilePath = "/upload/" + _FileName,
                    ContentType = _IFormFile.ContentType,
                    FileName = _FileName,
                    Length = _IFormFile.Length,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    CreatedBy = "Admin",
                    ModifiedBy = "Admin",
                };
                _context.AttachmentFile.Add(_AttachmentFile);
                await _context.SaveChangesAsync();
                return _AttachmentFile;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Tuple<byte[], string> GetDownloadDetails(Int64 id)
        {
            byte[] bytes = null;
            try
            {
                var _AttachmentFile = _context.AttachmentFile.Where(x => x.Id == id).SingleOrDefault();
                string _WebRootPath = _iHostingEnvironment.WebRootPath + _AttachmentFile.FilePath;
                bytes = File.ReadAllBytes(_WebRootPath);

                var _Tuple = new Tuple<byte[], string>(bytes, _AttachmentFile.FileName);
                return _Tuple;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}