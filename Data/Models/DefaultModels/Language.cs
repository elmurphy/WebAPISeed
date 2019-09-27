using Data.Models.BaseModels;
using Data.Models.BaseModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Models.DefaultModels
{
    public class Language : IdBase, ISortable
    {
        public string Name { get; set; }
        public string ShortName { get; set; }
        public int? Order { get; set; }
    }
}
