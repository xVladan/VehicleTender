using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.DataTransferObjects
{
    public class HomeTableDTO
    {
        public int Id { get; set; }
        public string TenderNo { get; set; }
        public string Dealer { get; set; }
        public string DealerName { get; set; }
        public string OpenDate { get; set; }
        public string CloseDate { get; set; }
    }
}
