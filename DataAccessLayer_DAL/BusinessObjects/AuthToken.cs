using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VehicleTender.v2.Models.DAL
{
    public class AuthToken
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string AccessToken { get; set; }
        public bool Active { get; set; }
    }
}