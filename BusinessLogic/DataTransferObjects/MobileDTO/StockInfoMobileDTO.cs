using System;

namespace BusinessLogic.DataTransferObjects.MobileDTO
{
    public class StockInfoMobileDTO
    {
        public int Id { get; set; }
        public int ModelLineId { get; set; }
        public int Mileage { get; set; }
        public double Price { get; set; }
        public string Comments { get; set; }
        public int LocationId { get; set; }
        public string RegNo { get; set; }
        public int Year { get; set; }
        public bool IsSold { get; set; }
        public DateTime? SaledDate { get; set; }
    }
}
