using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrandingSystem.Application.Dtos
{
    public class CountOrdersEnorllmentStatus
    {
        public int CountOrdersAccepted { get; set; }
        public int CountOrdersPending { get; set; }

        public int CountOrdersCanceled { get; set; }

      public  int CountOrdersTotal { get { return CountOrdersAccepted + CountOrdersPending + CountOrdersCanceled; } }

    }
}
