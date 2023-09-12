using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.DataTransferObjects
{
    public class TenderViewModel
    {
        public int Id { get; set; }
        public string CreatedDate { get; set; }
        public string UserId { get; set; }
        public string TenderNo { get; set; }
        public string OpenDate { get; set; }
        public string CloseDate { get; set; }
        public int StatusId { get; set; }
    }
}
