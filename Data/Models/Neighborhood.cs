using Data.Models.BaseModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Models
{
    public class Neighborhood : LocationSeed
    {
        public Guid DistrictId { get; set; }
        [ForeignKey("DistrictId")]
        public District District { get; set; }
    }
}
