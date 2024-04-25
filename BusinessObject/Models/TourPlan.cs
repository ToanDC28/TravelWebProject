using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObject.Models
{
    public partial class TourPlan
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PlanId { get; set; }
        public int? TourId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }

        public virtual Tour? Tour { get; set; }
    }
}
