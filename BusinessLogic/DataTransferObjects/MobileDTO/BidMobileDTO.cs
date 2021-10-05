using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.DataTransferObjects.MobileDTO
{
    public class BidMobileDTO
    {
        public int Id { get; set; }
        public int TenderUserId { get; set; }
        public int TenderStockId { get; set; }
        public double Price { get; set; }
        public bool isActive { get; set; }
        public bool IsWinningPrice { get; set; }
    }
}
