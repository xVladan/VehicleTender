using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DataAccessLayer_DAL
{
    public class TenderStatus
    {
        [Key]
        public int Id { get; set; }
        public string Type { get; set; }
    }
}