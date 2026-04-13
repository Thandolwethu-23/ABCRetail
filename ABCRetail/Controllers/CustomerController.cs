using ABCRetail.Services;
using Microsoft.AspNetCore.Mvc;

namespace ABCRetail.Controllers
{
    public class CustomerController : Controller
    {
        private readonly TableStorageService _tableService;

        public CustomerController(TableStorageService tableService)
        {
            _tableService = tableService;
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(string name, string email)
        {
            await _tableService.AddCustomerAsync(name, email);

            ViewBag.Message = "Customer added successfully!";
            return View();
        }
    }
}