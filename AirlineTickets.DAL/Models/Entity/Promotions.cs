using MayNghien.Common.Models;
using MayNghien.Common.Models.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineTickets.DAL.Models.Entity
{
    public class Promotions : BaseEntity
    {
        public string? Name {  get; set; }
        public string? PromotionInformation {  get; set; }
        public string? PromotionType { get; set; }
        public int? NumberOfSeats { get; set; }
        public bool? Stastus { get; set; }
        [ForeignKey("Customers")]
        public Guid? CustomersId { get; set; }
        [ForeignKey("CustomersId")]
        public Customers? Customers { get; set; }

    }
}
