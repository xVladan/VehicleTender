using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DataAccessLayer_DAL
{
    public class Manufacturer
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(20)]
        public string ManufacturerName { get; set; }
    }
}