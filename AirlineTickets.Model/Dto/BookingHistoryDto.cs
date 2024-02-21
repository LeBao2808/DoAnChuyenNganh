using MayNghien.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineTickets.Model.Dto
{
    public class BookingHistoryDto : BaseDto
    {
        public Guid BookTicketsId { get; set; }
        public Guid CustomersId { get; set; }
        public Guid FlightId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? BookingDate { get; set; }
        public int? Quantity { get; set; }
        public double? CorrespondingTicketPrices { get; set; }
        public int? TicketBookingStatus { get; set; }
    }
}
