using MayNghien.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineTickets.Model.Dto
{
    public class BookTicketsDto : BaseDto
    {
        public Guid CustomersId { get; set; }
        public Guid FlightId { get; set; }
        public int? Quantity { get; set; }
        public double? CorrespondingTicketPrices { get; set; }
        public int? TicketBookingStatus { get; set; }
    }
}
