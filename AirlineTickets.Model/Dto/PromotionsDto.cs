using MayNghien.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineTickets.Model.Dto
{
    public class PromotionsDto : BaseDto
    {
        public string? Name { get; set; }
        public string? PromotionInformation { get; set; }
        public string? PromotionType { get; set; }
        public int? NumberOfSeats { get; set; }
        public bool? Stastus { get; set; }
        public Guid? CustomersId { get; set; }
    }
}
