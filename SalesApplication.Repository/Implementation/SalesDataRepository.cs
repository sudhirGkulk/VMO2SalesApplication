using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SalesApplication.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using SalesApplication.Repository.Interface;
using SalesApplication.Models.Entities;

namespace SalesApplication.Repository.Implementation
{
    public class SalesDataRepository : ISalesDataRepository
    {
        public async Task<List<Sale>> GetSalesDataAsync(string filePath)
        {
            // In a real-world scenario, data would be retrieved from a database
            // For now, we simulate it by returning hardcoded values
            using (var reader = new StreamReader(filePath))
            using (var csvReader = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)))
            {
                csvReader.Read();
                csvReader.ReadHeader();
                var records = csvReader.GetRecords<Sale>().ToList();
                return records;
            }
            //var salesData = new List<Sale>
            //{
                
            //};

            //return await Task.FromResult(salesData);
        }
    }
}
