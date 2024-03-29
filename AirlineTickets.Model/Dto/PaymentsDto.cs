using MayNghien.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineTickets.Model.Dto
{
    public class PaymentsDto : BaseDto
    {
       
        public double? PayAmount { get; set; }
        public DateTime? PayDate { get; set; }
        public bool? PaymentStatus { get; set; }

        public string? PaymentMethods { get; set; }
    }
}
