using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayslipGenerator.Classes
{
    public class Payroll
    {
        public double DailyRate { get; set; }
        public double BasicSalary { get; set; }
        public double DaysIn { get; set; }
        public double Adjustments { get; set; }
        public double Absents { get; set; }
        public double Others { get; set; }
        public double TotalNet { get; set; }
        public double TotalDeduction { get; set; }
        public double TotalGross { get; set; }
    }

    public class DailyRates 
    {
        public Dictionary<string, double> Rates { get; set; }
    }

}
