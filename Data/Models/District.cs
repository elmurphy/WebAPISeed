using Data.Models.BaseModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Models
{
    public class District : LocationSeed
    {
        public Guid CityId { get; set; }
        [ForeignKey("CityId")]
        public City City { get; set; }
    }
}
