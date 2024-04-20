using MayNghien.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineTickets.Model.Dto
{
    public class AirplaneSeatsDto : BaseDto
    {
        public Guid FlightsId { get; set; }
        public int? Seats { get; set; }
        public bool? IsAirplane { get; set; }
    }
}
