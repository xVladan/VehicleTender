using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.DataTransferObjects
{
    public class StockDTO
    {
        public int Id { get; set; }
        public string Location { get; set; }
        public int ModelLineId { get; set; }
        public int LocationId { get; set; }
        public string ModelNo { get; set; }
        public string Manufacturer { get; set; }
        public int Mileage { get; set; }
        public double Price { get; set; }
        public string Comments { get; set; }
        public string RegNo { get; set; }
        public bool IsSold { get; set; }
        public int Year { get; set; }
        public string CarModel { get; set; }
        public string FullCarName { get; set; }
    }
}
