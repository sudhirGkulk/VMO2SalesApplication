using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SalesApplication.Repository.Interface;
using SalesApplication.Services.Services.Service;
using SalesApplication.Services.Services.Interfaces;
using SalesApplication.Models.Entities;


namespace SaleApplication.Service.Tests
{
    [TestClass]
    public class SalesServiceTests
    {
        private Mock<ISalesDataRepository>? _mockSalesDataRepository;
        private ISalesService? _salesService;

        [TestInitialize]
        public void TestInitialize()
        {
            // Initialize the mock object and service before each test
            _mockSalesDataRepository = new Mock<ISalesDataRepository>();
            _salesService = new SalesService(_mockSalesDataRepository.Object);
        }

        [TestMethod]
        public async Task GetSalesDataAsync_ReturnsListOfSales()
        {
            // Arrange
            var salesData = new List<Sale>
            {
                new Sale { Segment = "Government", Country = "UK", Product = "Product1", UnitsSold = 4545.5, SalePrice = 65.00m },
                new Sale { Segment = "Government", Country = "Germany", Product = "Product2", UnitsSold = 2333, SalePrice = 77.00m }
            };

            _mockSalesDataRepository.Setup(repo => repo.GetSalesDataAsync("")).ReturnsAsync(salesData);

            // Act
            var result = await _salesService!.GetSalesDataAsync("");

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("Government", result[0].Segment);
            Assert.AreEqual(4545.5, result[0].UnitsSold);
        }

        [TestMethod]
        public async Task CalculateSalesSummaryAsync_CalculatesCorrectSummary()
        {
            // Arrange
            var salesData = new List<Sale>
            {
                new Sale { Segment = "Government", Country = "UK", Product = "Product1", UnitsSold = 4545.5, SalePrice = 65.00m },
                new Sale { Segment = "Government", Country = "Germany", Product = "Product2", UnitsSold = 2333, SalePrice = 77.00m }
            };

            _mockSalesDataRepository.Setup(repo => repo.GetSalesDataAsync("")).ReturnsAsync(salesData);

            // Act
            var summary = await _salesService!.CalculateSalesSummaryAsync(salesData);

            // Assert
            Assert.AreEqual(4545.5 + 2333, summary.TotalUnitsSold);  // TotalUnitsSold = sum of all units sold
            Assert.AreEqual(2, summary.TotalTransactions);         // TotalTransactions = count of sales
            Assert.AreEqual(20.00m, summary.AvgSalePrice);        // AvgSalePrice = TotalSales / TotalUnitsSold
        }
    }
}
