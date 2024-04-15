using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class Region
    {
        public Region()
        {
            Destinations = new HashSet<Destination>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }

        public virtual ICollection<Destination> Destinations { get; set; }
    }
}
