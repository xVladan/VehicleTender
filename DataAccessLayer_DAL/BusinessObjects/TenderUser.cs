using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using VehicleTender.v2.Models;

namespace DataAccessLayer_DAL
{
    public class TenderUser
    {
        [Key]
        public int Id { get; set; }
        [Display(Name ="Tender")]
        public int TenderId { get; set; }
        [Display(Name ="User")]
        public string UserId { get; set; }
        public bool isDeleted { get; set; }

        [ForeignKey("TenderId")]
        public virtual Tender Tender { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }
    }
}