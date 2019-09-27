using Data.Models.BaseModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Models.DefaultModels
{
    public class Tag : IdBase
    {
        public string Content { get; set; }
    }
}
