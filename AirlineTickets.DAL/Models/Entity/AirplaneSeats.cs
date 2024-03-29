using MayNghien.Common.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineTickets.DAL.Models.Entity
{
    public class AirplaneSeats : BaseEntity
    {
        public int? Seats {  get; set; }
        public bool? IsAirplane { get; set; }
    }
}
