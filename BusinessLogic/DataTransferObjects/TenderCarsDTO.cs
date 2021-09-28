using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.DataTransferObjects
{
    public class TenderCarsDTO
    {
        public int Id { get; set; }
        public string RegNo {get; set; }
        public int Year { get; set; }
        public string Make { get; set; }
        public string CarLine { get; set; }
        public string Model { get; set; }
        public double Mileage { get; set; }
        public string Comments { get; set; }
    }
}
