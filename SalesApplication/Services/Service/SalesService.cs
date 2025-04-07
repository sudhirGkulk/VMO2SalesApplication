
using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using SalesApplication.Web.Models;
using SalesApplication.Web.Services.Interfaces;



namespace SalesApplication.Web.Services.Service;

public class SalesService : ISalesService
{

    public List<Sale> GetSalesData(string filePath)
    {
        using (var reader = new StreamReader(filePath))
        using (var csvReader = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture) ))
        {
            csvReader.Read();
            csvReader.ReadHeader();
            var records = csvReader.GetRecords<Sale>().ToList();
            return records;
        }
    }


}