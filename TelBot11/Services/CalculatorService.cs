using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelBot11.Services
{
    public class CalculatorService
    {
        public double GetSum(List<Double> args)
        {
            return args.Sum();
        }
    }
}
