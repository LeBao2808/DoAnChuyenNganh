using MayNghien.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineTickets.Model.Dto
{
    public class BookingsDto:BaseDto
    {
        public Guid? CustomersId { get; set; }
        public Guid? TicketsId { get; set; }
        public Guid? LuggagesId { get; set; }
        public Guid? AirplaneSeatsId {  get; set; }
        public double? CorrespondingTicketPrices { get; set; }
        public int? TicketBookingStatus { get; set; }
        public DateTime? BookingDate { get; set; }
        public int? Quantity { get; set; }
        public CustomersDto? Customers {  get; set; } 
        public int? Seats { get; set; }
    }
}
