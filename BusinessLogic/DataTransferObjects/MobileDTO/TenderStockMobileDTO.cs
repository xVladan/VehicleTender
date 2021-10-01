using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.DataTransferObjects.MobileDTO
{
    public class TenderStockMobileDTO
    {
        public int Id { get; set; }
        public int StockId { get; set; }
        public int TenderId { get; set; }
        public bool isDeleted { get; set; }
        public DateTime? SaleDate { get; set; }
    }
}
