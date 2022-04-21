using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathIntegralMethod.Classes
{
    public interface ICalculator
    {
        public double Calculate(int count, double downLimit, double upLimit, out double time, Func<double, double> integral);
        public double ParallelCalculate(int count, double downLimit, double upLimit, out double time, Func<double, double> integral);
    }
}
