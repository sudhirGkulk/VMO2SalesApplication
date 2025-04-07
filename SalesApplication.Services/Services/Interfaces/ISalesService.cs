using SalesApplication.Models.DTOs;
using SalesApplication.Models.Entities;

namespace SalesApplication.Services.Services.Interfaces
{
    public interface ISalesService
    {
        Task<List<Sale>> GetSalesDataAsync(string filePath);
        Task<ResultsDTO> CalculateSalesSummaryAsync(List<Sale> salesRecords);

    }

}
