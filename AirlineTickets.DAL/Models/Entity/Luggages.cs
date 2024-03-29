using MayNghien.Common.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineTickets.DAL.Models.Entity
{
    public class Luggages : BaseEntity
    {
        public string? name {  get; set; }
        public string? description { get; set; }
        public double? Price {  get; set; }

    }
}
