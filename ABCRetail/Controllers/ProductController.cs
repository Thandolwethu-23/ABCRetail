using ABCRetail.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace ABCRetail.Controllers
{
    public class ProductController : Controller
    {
        private readonly BlobStorageService _blobService;
        private readonly QueueStorageService _queueService;

        // Constructor
        public ProductController(BlobStorageService blobService, QueueStorageService queueService)
        {
            _blobService = blobService;
            _queueService = queueService;
        }

        // GET: Upload page
        public IActionResult Upload()
        {
            return View();
        }

        // POST: Upload image + send queue message
        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                // Upload image to Blob Storage
                string imageUrl = await _blobService.UploadFileAsync(file);

                // Send message to Queue Storage
                await _queueService.SendMessageAsync("Image uploaded: " + file.FileName);

                // Display image on page
                ViewBag.ImageUrl = imageUrl;
                ViewBag.Message = "Image uploaded successfully!";
            }
            else
            {
                ViewBag.Message = "Please select a file.";
            }

            return View();
        }
    }
}