using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace FileUploader.Models.AttachmentFileViewModel
{
    // View mapping class
    public class AttachmentFileCRUDViewModel : EntityBase
    {
        [Display(Name = "SL")]
        [Required]
        public Int64 Id { get; set; }
        public string FilePath { get; set; }
        public string ContentType { get; set; }
        public string FileName { get; set; }
        public IFormFile AttachmentFile { get; set; }
        public Int64 Length { get; set; }

        public static implicit operator AttachmentFileCRUDViewModel(AttachmentFile _AttachmentFile)
        {
            return new AttachmentFileCRUDViewModel
            {
                Id = _AttachmentFile.Id,
                FilePath = _AttachmentFile.FilePath,
                ContentType = _AttachmentFile.ContentType,
                FileName = _AttachmentFile.FileName,
                Length = _AttachmentFile.Length,
                
                CreatedDate = _AttachmentFile.CreatedDate,
                ModifiedDate = _AttachmentFile.ModifiedDate,
                CreatedBy = _AttachmentFile.CreatedBy,
                ModifiedBy = _AttachmentFile.ModifiedBy,
                Cancelled = _AttachmentFile.Cancelled,
            };
        }

        public static implicit operator AttachmentFile(AttachmentFileCRUDViewModel vm)
        {
            return new AttachmentFile
            {
                Id = vm.Id,
                FilePath = vm.FilePath,
                ContentType = vm.ContentType,
                FileName = vm.FileName,
                Length = vm.Length,
                
                CreatedDate = vm.CreatedDate,
                ModifiedDate = vm.ModifiedDate,
                CreatedBy = vm.CreatedBy,
                ModifiedBy = vm.ModifiedBy,
                Cancelled = vm.Cancelled,
            };
        }
    }
}

