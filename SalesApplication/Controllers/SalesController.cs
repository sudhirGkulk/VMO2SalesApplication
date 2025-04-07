using Microsoft.AspNetCore.Mvc;
using SalesApplication.Web.Models;
using SalesApplication.Web.Services.Service;

namespace SalesApplication.Web.Controllers
{

    public class SalesController : Controller
    {
        private readonly SalesService _salesService;
        private readonly ILogger<SalesController> _logger;

        public SalesController(SalesService salesService, ILogger<SalesController> logger)
        {
            _salesService = salesService;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var salesData = _salesService.GetSalesData("data.csv");

            var totalSales = 10; // salesData.Sum(s => s.UnitsSold * s.SalePrice); // Total Sales
            var totalUnitsSold = salesData.Sum(s => s.UnitsSold); // Total Units Sold
            var totalTransactions = salesData.Count; // Total Number of Transactions
            var avgSalePrice = salesData.Average(s => s.SalePrice); // Average Sale Price

            ResultsDTO summary = new ResultsDTO
            {
                TotalSales = totalSales,
                TotalUnitsSold = totalUnitsSold,
                TotalTransactions = totalTransactions,
                AvgSalePrice = avgSalePrice
            };

            return View(summary);
        }
    }


}


