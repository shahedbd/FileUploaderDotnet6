using FileUploader.Data;
using FileUploader.Models;
using FileUploader.Models.AttachmentFileViewModel;
using FileUploader.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace FileUploader.Controllers
{
    public class AttachmentFileController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ICommon _iCommon;

        public AttachmentFileController(ApplicationDbContext context, ICommon iCommon)
        {
            _context = context;
            _iCommon = iCommon;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var _GetGridItem = GetGridItem().ToList();
            return View(_GetGridItem);
        }

        private IQueryable<AttachmentFileCRUDViewModel> GetGridItem()
        {
            try
            {
                return (from _AttachmentFile in _context.AttachmentFile
                        where _AttachmentFile.Cancelled == false
                        select new AttachmentFileCRUDViewModel
                        {
                            Id = _AttachmentFile.Id,
                            ContentType = _AttachmentFile.ContentType,
                            FileName = _AttachmentFile.FileName,
                            Length = _AttachmentFile.Length,
                            CreatedDate = _AttachmentFile.CreatedDate,
                        }).OrderByDescending(x => x.Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IActionResult> Details(Int64 id)
        {
            AttachmentFileCRUDViewModel vm = await _context.AttachmentFile.FirstOrDefaultAsync(m => m.Id == id);
            return PartialView("_Details", vm);
        }

        public IActionResult AddEdit(int id)
        {
            AddNewFileViewModel vm = new AddNewFileViewModel();
            return PartialView("_AddEdit", vm);
        }

        [HttpPost]
        public async Task<JsonResult> AddNewAttachmentFile(AddNewFileViewModel vm)
        {
            AttachmentFile _AttachmentFile = null;

            if (ModelState.IsValid)
            {
                try
                {
                    foreach (var item in vm.AttachmentFile)
                    {
                        _AttachmentFile = await _iCommon.AddAttachmentFile(item);
                    }
                }
                catch (Exception ex)
                {
                    TempData["errorAlert"] = "Operation failed.";
                    throw ex;
                }
            }
            var successAlert = "File Uploaded Successfully. Total File Uploaded: " + vm.AttachmentFile.Count;
            TempData["successAlert"] = successAlert;
            return new JsonResult(successAlert);
        }

        public FileResult DownloadFile(Int64 id)
        {
            try
            {
                var _GetDownloadDetails = _iCommon.GetDownloadDetails(id);
                return File(_GetDownloadDetails.Item1, "application/octet-stream", _GetDownloadDetails.Item2);
            }
            catch (Exception ex)
            {
                TempData["errorAlert"] = "Operation failed." + ex.Message;
                throw ex;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Int64 id)
        {
            try
            {
                var _AttachmentFile = await _context.AttachmentFile.FindAsync(id);
                _AttachmentFile.ModifiedDate = DateTime.Now;
                _AttachmentFile.ModifiedBy = "Admin";
                _AttachmentFile.Cancelled = true;

                _context.Update(_AttachmentFile);
                await _context.SaveChangesAsync();
                return new JsonResult(_AttachmentFile);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
