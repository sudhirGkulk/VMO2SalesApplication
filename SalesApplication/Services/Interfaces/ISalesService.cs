namespace SalesApplication.Web.Services.Interfaces;
using SalesApplication.Web.Models;
public interface ISalesService
{
    public List<Sale> GetSalesData(string filePath);
}