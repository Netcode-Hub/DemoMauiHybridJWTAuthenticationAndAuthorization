using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.Models
{
    public class ServiceResponse
    {
        public bool Flag { get; set; }
        public string Message { get; set; } = string.Empty;
        public string? Token { get; set; } = string.Empty;
    }
}
