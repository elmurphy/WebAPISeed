using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.VirtualModels
{
    public class VirtualImage
    {
        public VirtualImage(string value)
        {
            Path = value;
        }
        public string Directory { get; set; } = "/uploads/";
        public string Path { get; set; }
        public string GetAllPath() => Directory + Path;
    }
}
