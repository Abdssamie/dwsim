using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DWSIM.Simulate365.Models
{
    public class BeforeLogoutEventArgs : EventArgs
    {
        public bool Cancel { get; set; }
        public string Reason { get; set; }
    }
}
