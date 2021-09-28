using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataAccessLayer_DAL
{
    public class Location
    {
        [Key]
        public int Id { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
    }
}