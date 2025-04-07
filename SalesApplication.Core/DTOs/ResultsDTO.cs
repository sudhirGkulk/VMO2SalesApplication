namespace SalesApplication.Models.DTOs
{
    public class ResultsDTO
    {
        public decimal TotalSales { get; set; }
        public double TotalUnitsSold { get; set; }
        public int TotalTransactions { get; set; }
        public decimal AvgSalePrice { get; set; }

    }
}
