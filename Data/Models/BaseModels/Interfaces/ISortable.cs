using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Models.BaseModels.Interfaces
{
    public interface ISortable
    {
        int? Order { get; set; }
    }
}
