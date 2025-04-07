
using SalesApplication.Services.Services.Interfaces;
using SalesApplication.Models.Entities;
using SalesApplication.Models.DTOs;
using SalesApplication.Repository.Interface;


namespace SalesApplication.Services.Services.Service

{
    public class SalesService : ISalesService
    {
        private readonly ISalesDataRepository _salesDataRepository;


        public SalesService(ISalesDataRepository salesDataRepository)
        {
            _salesDataRepository = salesDataRepository;
        }



        public async Task<List<Sale>> GetSalesDataAsync(string filePath)
        {
            // Retrieve sales data from the repository (this could be a database or file)
            var salesData = await _salesDataRepository.GetSalesDataAsync(filePath);
            return salesData;
        }

        public async Task<ResultsDTO> CalculateSalesSummaryAsync(List<Sale> salesRecords)
        {
            var totalSales = salesRecords.Sum(record => record.SalePrice * (decimal)record.UnitsSold);
            var totalUnitsSold = salesRecords.Sum(record => record.UnitsSold);
            var totalTransactions = salesRecords.Count();
            var avgSalePrice = totalSales / (decimal)totalUnitsSold;

            return new ResultsDTO
            {
                TotalSales = totalSales,
                TotalUnitsSold = totalUnitsSold,
                TotalTransactions = totalTransactions,
                AvgSalePrice = avgSalePrice
            };
        }



        //public List<Sale> GetSalesData(string filePath)
        //{
        //    using (var reader = new StreamReader(filePath))
        //    using (var csvReader = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture) ))
        //    {
        //        csvReader.Read();
        //        csvReader.ReadHeader();
        //        var records = csvReader.GetRecords<Sale>().ToList();
        //        return records;
        //    }
        //}


    }

}

