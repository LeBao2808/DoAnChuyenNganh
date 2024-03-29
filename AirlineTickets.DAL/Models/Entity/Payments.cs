using MayNghien.Common.Models.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineTickets.DAL.Models.Entity
{
    public class Payments : BaseEntity
    {
        [ForeignKey("Bookings")]
        public Guid BookingsId { get; set; }
        [ForeignKey("BookingsId")]
        public Bookings? Bookings { get; set; }

        public double? PayAmount { get; set; }
        public DateTime? PayDate { get; set; }
        public bool? PaymentStatus {  get; set; }

        public string? PaymentMethods { get; set; }
    }
}
