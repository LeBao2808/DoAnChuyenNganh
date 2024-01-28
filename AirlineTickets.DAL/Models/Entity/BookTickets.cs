using MayNghien.Common.Models.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace AirlineTickets.DAL.Models.Entity
{
    public class BookTickets : BaseEntity
    {
        [ForeignKey("Customers")]
        public Guid CustomersId { get; set; }
        [ForeignKey("CustomersId")]
        public Customers? Customers { get; set; }

        [ForeignKey("Flight")]
        public Guid FlightId { get; set; }
        [ForeignKey("FlightId")]
        public Flight? Flight { get; set; }

        public int? Quantity {  get; set; }
        public double? CorrespondingTicketPrices { get; set; }
        public int? TicketBookingStatus { get; set; }
    }
}
