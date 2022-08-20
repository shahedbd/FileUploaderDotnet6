using FileUploader.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace FileUploader.Services
{
    public interface ICommon
    {
        string UploadedFile(IFormFile ProfilePicture);
        Tuple<byte[], string> GetDownloadDetails(Int64 id);
        Task<AttachmentFile> AddAttachmentFile(IFormFile _IFormFile);
    }
}
