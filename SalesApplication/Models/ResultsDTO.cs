namespace SalesApplication.Web.Models
{
    public class ResultsDTO
    {
        public int TotalSales { get; set; }
        public double TotalUnitsSold { get; set; }
        public int TotalTransactions { get; set; }
        public decimal AvgSalePrice { get; set; }

    }
}
