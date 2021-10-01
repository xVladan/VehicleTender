using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DataAccessLayer_DAL
{
    public class Bid
    {
        [Key]
        public int Id { get; set; }
        [Display(Name ="Tender User")]
        public int TenderUserId { get; set; }
        [Display(Name ="Tender Stock")]
        public int TenderStockId { get; set; }
        public double Price { get; set; }
        public bool isActive { get; set; }
        public bool IsWinningPrice { get; set; }

        [ForeignKey("TenderUserId")]
        public virtual TenderUser User { get; set; }
        [ForeignKey("TenderStockId")]
        public virtual TenderStock Stock { get; set; }
    }
}