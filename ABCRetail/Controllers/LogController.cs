using Microsoft.AspNetCore.Mvc;
using ABCRetail.Services;
using System;
using System.Threading.Tasks;

namespace ABCRetail.Controllers
{
    public class LogController : Controller
    {
        private readonly FileStorageService _fileService;

        public LogController(FileStorageService fileService)
        {
            _fileService = fileService;
        }

        public async Task<IActionResult> TestLog()
        {
            await _fileService.SaveLogAsync(
                "Log entry at " + DateTime.Now
            );

            return Content("Log written successfully");
        }
    }
}