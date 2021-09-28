using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.DataTransferObjects
{
    public class TenderBids
    {
        public int Id { get; set; }
        public int TenderUserId { get; set; }
        public int TenderStockId { get; set; }
        public double Price { get; set; }
        public bool IsWiningPrice { get; set; }
    }
}
