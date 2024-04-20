using MayNghien.Common.Models.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineTickets.DAL.Models.Entity
{
    public class AirplaneSeats : BaseEntity
    {

        [ForeignKey("Flights")]
        public Guid FlightsId { get; set; }
        [ForeignKey("FlightsId")]
        public Flights? Flights { get; set; }
        public int? Seats {  get; set; }
        public bool? IsAirplane { get; set; }
    }
}
