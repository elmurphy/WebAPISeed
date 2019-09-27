using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.VirtualModels
{
    public class VirtualUploadModel
    {
        public IFormFile File { get; }
        public string Name { get; set; }
        public List<string> Tags { get; set; }
    }
}
