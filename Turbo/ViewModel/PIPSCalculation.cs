using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Risen.ViewModel
{
    public class PIPSCalculation
    {
        public int PIPSWonCount { get; set; }
        public int PIPSLossCount { get; set; }
        public int PIPSWonSum { get; set; }
        public int PIPSLossSum { get; set; }

        public PIPSCalculation()
        {
            PIPSWonCount = 0;
            PIPSLossCount = 0;
            PIPSWonSum = 0;
            PIPSLossSum = 0;
        }
    }
}