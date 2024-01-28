using MayNghien.Common.Models.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineTickets.DAL.Models.Entity
{
    public class Flight : BaseEntity
    {
        public string? FlightNumber { get; set; }
        [ForeignKey("Partner")]
        public Guid PartnerId { get; set; }
        [ForeignKey("PartnerId")]
        public Partner? Partner { get; set; }

        public string? StartingPoint { get; set; }
        public string? EndingPoint { get; set; }
        public int? FlightTime { get; set; }
        public DateTime? StartDate { get; set; }
        public int? NumberOfEmptySeats { get; set; }
        public double? TicketPrice{ get; set; }
    }
}
