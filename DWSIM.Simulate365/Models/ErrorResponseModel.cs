using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DWSIM.Simulate365.Models
{
    public class ErrorResponseModel
    {
        public IEnumerable<string> Errors { get; set; }

        public Dictionary<string, IEnumerable<string>> ValidationErrors { get; set; }
    }
}
