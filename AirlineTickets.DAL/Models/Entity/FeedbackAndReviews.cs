﻿using MayNghien.Common.Models.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineTickets.DAL.Models.Entity
{
    public class FeedbackAndReviews : BaseEntity
    {
        [ForeignKey("Customers")]
        public Guid CustomersId { get; set; }
        [ForeignKey("CustomersId")]
        public Customers? Customers { get; set; }

        [ForeignKey("Flights")]
        public Guid FlightsId { get; set; }
        [ForeignKey("FlightsId")]
        public Flights? Flights { get; set; }

        public string? Feedback {  get; set; }
        public double? PointEvaluation {  get; set; }
        public DateTime? ReactionTime { get; set; }
    }
}
