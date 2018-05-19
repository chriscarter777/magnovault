using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace magnovault.Authorize.Net
{
     public static class Responses
     {
          public static Dictionary<int, string> ResponseCodes = new Dictionary<int, string>()
          {
               { 1, "Approved"},
               { 2, "Declined"},
               { 3, "Error"},
               { 4, "Held For Review"}
          };
     }
}
