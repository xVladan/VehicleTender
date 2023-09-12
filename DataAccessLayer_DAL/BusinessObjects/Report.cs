using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer_DAL.BusinessObjects
{
    public class Report
    {
        public int Id { get; set; }
        public string TenderNo { get; set; }
        public string TenderOpen { get; set; }
        public string TenderClosed { get; set; }
        public string BidderName { get; set; }
        public string Price { get; set; }
    }
}
