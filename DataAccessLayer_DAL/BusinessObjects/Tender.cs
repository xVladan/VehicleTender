using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using VehicleTender.v2.Models;

namespace DataAccessLayer_DAL
{
    public class Tender
    {
        [Key]
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        [Display(Name ="Created By")]
        public string UserId { get; set; }
        [StringLength(50)]
        public string TenderNo { get; set; }
        public DateTime OpenDate { get; set; }
        public DateTime CloseDate { get; set; }
        [Display(Name ="Status")]
        public int StatusId { get; set; }


        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }
        [ForeignKey("StatusId")]
        public virtual TenderStatus Status { get; set; }
    }
}