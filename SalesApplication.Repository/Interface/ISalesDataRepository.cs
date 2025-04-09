using SalesApplication.Models.Entities;

namespace SalesApplication.Repository.Interface
{
    public interface ISalesDataRepository
    {    
        Task<List<Sale>> GetSalesDataAsync(string filePath);
    }

}
