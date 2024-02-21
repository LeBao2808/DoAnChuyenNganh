using MayNghien.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineTickets.Model.Dto
{
    public class FeedbackAndReviewsDto : BaseDto
    {
        public Guid CustomersId { get; set; }
        public Guid FlightId { get; set; }
        public string? Feedback { get; set; }
        public double? PointEvaluation { get; set; }
        public DateTime? ReactionTime { get; set; }
    }
}
