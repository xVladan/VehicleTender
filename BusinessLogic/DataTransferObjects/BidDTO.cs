using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.DataTransferObjects
{
    public class BidDTO
    {
        public int Id { get; set; }
        public string BidderName { get; set; }
        public int TenderStockId { get; set; }
        public int TenderUserId { get; set; }
        public int StockId { get; set; }
        public double Price { get; set; }
        public bool IsWinningPrice { get; set; }
    }
}
