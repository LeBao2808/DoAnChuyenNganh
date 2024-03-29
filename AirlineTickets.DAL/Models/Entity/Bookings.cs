using MayNghien.Common.Models.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineTickets.DAL.Models.Entity
{
   public class Bookings:BaseEntity
    {
        [ForeignKey("Customers")]
        public Guid CustomersId { get; set; }
        [ForeignKey("CustomersId")]
        public Customers? Customers { get; set; }

        [ForeignKey("Luggages")]
        public Guid? LuggagesId { get; set; }
        [ForeignKey("LuggagesId")]
        public Luggages? Luggages { get; set; }

        [ForeignKey("Tickets")]
        public Guid? TicketsId { get; set; }
        [ForeignKey("TicketsId")]
        public Tickets? Tickets { get; set; }
        public double? CorrespondingTicketPrices { get; set; }
        public int? TicketBookingStatus { get; set; }
        public DateTime? BookingDate { get; set; }
        public int? Quantity { get; set; }  
      

    }
}
