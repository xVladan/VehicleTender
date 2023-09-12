using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DataAccessLayer_DAL
{
    public class CarModel
    {
        [Key]
        public int Id { get; set; }
        public string ModelName { get; set; }
        public string ModelNo { get; set; }
        [Display(Name = "Manufacturer")]
        public virtual int ManufacturerId { get; set; }

        [ForeignKey("ManufacturerId")]
        public virtual Manufacturer Manufacturer { get; set; }
    }
}