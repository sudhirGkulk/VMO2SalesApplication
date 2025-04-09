using Microsoft.VisualStudio.TestTools.UnitTesting;
using SalesApplication.Repository.Implementation;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace SalesApplication.Repository.Tests
{
    [TestClass]
    public class SalesDataRepositoryTests
    {

        private SalesDataRepository? _salesDataRepository;

        [TestInitialize]
        public void TestInitialize()
        {
            // Initialize the repository before each test
            _salesDataRepository = new SalesDataRepository();
        }

        [TestMethod]
        public async Task GetSalesDataAsync_ReturnsListOfSalesRecords()
        {
            // Act
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            var salesData = await _salesDataRepository.GetSalesDataAsync("");
#pragma warning restore CS8602 // Dereference of a possibly null reference.

            // Assert
            Assert.IsNotNull(salesData);
            Assert.AreEqual(4, salesData.Count); // We know that there are 4 records in the hardcoded list
            Assert.AreEqual("Government", salesData[0].Segment);
            Assert.AreEqual("Carretera", salesData[0].Product);
            Assert.AreEqual(1618.5, salesData[0].UnitsSold);
        }

    }
}
