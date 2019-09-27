using System;
using System.Collections.Generic;
using System.Text;

namespace Data.VirtualModels
{
    public class ResponseModal
    {
        public int StatusCode { get; set; }
        public object Data { get; set; } = null;
        private string message { get; set; } = null;
        public string Message { get => !string.IsNullOrEmpty(message) ? message : Result ? "İşlem başarılı" : "İşlem başarısız"; set => message = value; }
        private bool result { get; set; } = false;
        public bool Result { get => !result ? Data != null : result; set => result = value; }
        public bool HasMessage { get => !string.IsNullOrEmpty(Message); }
    }
}
