using MayNghien.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineTickets.Model.Dto
{
    public class LuggagesDto : BaseDto
    {
        public string? name { get; set; }
        public string? description { get; set; }
        public double? Price { get; set; }
    }
}
