using MayNghien.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineTickets.Model.Dto
{
    public class FlightDto : BaseDto
    {
        public string? FlightNumber { get; set; }
        public Guid PartnerId { get; set; }
        public string? StartingPoint { get; set; }
        public string? EndingPoint { get; set; }
        public int? FlightTime { get; set; }
        public DateTime? StartDate { get; set; }
        public int? NumberOfEmptySeats { get; set; }
        public double? TicketPrice { get; set; }
    }
}
