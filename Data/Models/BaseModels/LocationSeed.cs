using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Data.Models.BaseModels
{
    public abstract class LocationSeed : IdBase
    {
        [MaxLength(64)]
        public string Name { get; set; }
    }
}
