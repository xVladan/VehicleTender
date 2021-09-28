using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DataAccessLayer_DAL
{
    public class TenderStock
    {
        [Key]
        public int Id { get; set; }
        [Display(Name ="Stock")]
        public int StockId { get; set; }
        [Display(Name ="Tender")]
        public int TenderId { get; set; }
        public DateTime? SaleDate { get; set; }

        [ForeignKey("StockId")]
        public virtual StockInfo Stock { get; set; }
        [ForeignKey("TenderId")]
        public virtual Tender Tender { get; set; }
    }
}