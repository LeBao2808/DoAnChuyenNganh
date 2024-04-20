using MayNghien.Common.Models.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineTickets.DAL.Models.Entity
{
    public class Flights : BaseEntity
    {
        public string? FlightNumber { get; set; }
        [ForeignKey("Airlines")]
        public Guid AirlinesId { get; set; }
        [ForeignKey("AirlinesId")]
        public Airlines? Airlines { get; set; }
        public int TotalNumberOfSeats { get; set; }
        public string? StartingPoint { get; set; }
        public string? EndingPoint { get; set; }
        public int? FlightTime { get; set; }
        public int? FlightTimeEnd { get; set; }
        public DateTime? StartDate { get; set; }
        public int? NumberOfEmptySeats { get; set; }
        public double? TicketPrice{ get; set; }
        public int? CountStatus { get; set; }
    }
}
