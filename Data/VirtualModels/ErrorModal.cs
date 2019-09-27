using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Data.VirtualModels
{
    public class ErrorModal
    {
        public HttpStatusCode Status { get; set; }
        public string Message { get; set; } = null;
        public string Description { get; set; } = null;
        public bool HasDescription { get => !string.IsNullOrEmpty(Description); }
    }
}
