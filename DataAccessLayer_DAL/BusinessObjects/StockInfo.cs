using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DataAccessLayer_DAL
{
    public class StockInfo
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Model Line")]
        public int ModelLineId { get; set; }
        public int Mileage { get; set; }
        public double Price { get; set; }
        public string Comments { get; set; }
        [Display(Name = "Location")]
        public int LocationId { get; set; }
        [StringLength(20)]
        public string RegNo { get; set; }
        public int Year { get; set; } 
        public bool IsSold { get; set; }
        public DateTime? SaledDate { get; set; }

        [ForeignKey("ModelLineId")]
        public virtual CarModel Car { get; set; }
        [ForeignKey("LocationId")]
        public virtual Location Location { get; set; }

    }
}