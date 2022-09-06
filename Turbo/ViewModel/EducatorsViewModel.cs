using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Risen.ViewModel
{
    public class EducatorsViewModel
    {
        public int employeeId { get; set; }
        public string employeeImage { get; set; }
        public string employeeName { get; set; }
        public int pointsWin { get; set; }
        public int pointsLoss { get; set; }
        public bool status { get; set; }
        public float wonPercentage { get; set; }
        public float lossPercentage { get; set; }
        public float wonSum { get; set; }
        public float lossSum { get; set; }
        public EducatorsViewModel()
        {
            employeeId = 0;
            employeeName = "";
            employeeImage = "";
            pointsWin = 0;
            pointsLoss = 0;
            status = true;
            wonPercentage = 0;
            lossPercentage = 0;
            wonSum =0;
            lossSum = 0;
        }
    }
}