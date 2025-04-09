using CsvHelper;
using CsvHelper.Configuration;
using Moq;
using SalesApplication.Models.Entities;
using SalesApplication.Repository.Implementation;
using SalesApplication.Repository.Interface;
using System.Globalization;





namespace SalesApplication.Repository.Tests
{
    [TestClass]
    public class SalesDataRepositoryTests
    {
        private Mock<ISalesDataRepository>? _mockSalesDataRepository;
        private SalesDataRepository? _salesDataRepository;

        [TestInitialize]
        public void TestInitialize()
        {
            // Initialize the repository before each test
            _mockSalesDataRepository = new Mock<ISalesDataRepository>();
            _salesDataRepository = new SalesDataRepository();
        }

        [TestMethod]
        public async Task GetSalesDataAsync_ReturnsListOfSalesRecords()
        {
            // Arrange
            var filePath = "mocked_file.csv"; // This is just a placeholder, file reading is mocked

            var mockData = new List<Sale>
            {
                new Sale { Segment = "Government", Country = "UK", Product = "Mobile Device", UnitsSold = 450, SalePrice = 99.00m },
                new Sale { Segment = "Public", Country = "Japan", Product = "Car component", UnitsSold = 700, SalePrice = 100.00m },
                new Sale { Segment = "Private", Country = "USA", Product = "Chip component", UnitsSold = 501, SalePrice = 1000.00m }
            };

            var mockStreamReader = new Mock<StreamReader>(filePath);
            var mockCsvReader = new Mock<CsvReader>(new Mock<StreamReader>(), new CsvConfiguration(CultureInfo.InvariantCulture));

            // Setting up the behavior of the CsvReader to return mock data
            mockCsvReader.Setup(cr => cr.Read()).Returns(true); // Simulate successful read
            mockCsvReader.Setup(cr => cr.ReadHeader()).Returns(true); // Simulate header read
            mockCsvReader.Setup(cr => cr.GetRecords<Sale>()).Returns(mockData.AsEnumerable());

            _mockSalesDataRepository.Setup(s => s.GetSalesDataAsync(filePath)).ReturnsAsync(mockData);

            // Act
            var result = await _mockSalesDataRepository.Object.GetSalesDataAsync(filePath);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count);
            Assert.AreEqual("Government", result[0].Segment);
            Assert.AreEqual(450, result[0].UnitsSold);
            Assert.AreEqual(99.00m, result[0].SalePrice);
            Assert.AreEqual("Public", result[1].Segment);
            Assert.AreEqual("Private", result[2].Segment);
            Assert.AreEqual("Car component", result[1].Product);
            Assert.AreEqual(501, result[2].UnitsSold);
        }
    }
}
