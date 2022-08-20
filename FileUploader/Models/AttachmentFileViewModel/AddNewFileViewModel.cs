namespace FileUploader.Models.AttachmentFileViewModel
{
    public class AddNewFileViewModel
    {
        public Int64 Id { get; set; }
        public IList<IFormFile> AttachmentFile { get; set; }
    }
}
