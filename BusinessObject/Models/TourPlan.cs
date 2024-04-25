using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class TourPlan
    {
        public int PlanId { get; set; }
        public int? TourId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual Tour Tour { get; set; }
    }
}
