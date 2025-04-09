using Microsoft.AspNetCore.Mvc;
using SalesApplication.Models.DTOs;
using SalesApplication.Services.Services.Interfaces;

namespace SalesApplication.Web.Controllers
{

    public class SalesController : Controller
    {
        private readonly ISalesService _salesService;
        private readonly ILogger<SalesController> _logger;

        public SalesController(ISalesService salesService, ILogger<SalesController> logger)
        {
            _salesService = salesService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var salesData = await _salesService.GetSalesDataAsync("data.csv");

            var summary = await _salesService.CalculateSalesSummaryAsync(salesData);

            return View(summary);
        }
    }


}


