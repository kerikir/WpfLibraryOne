using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MathIntegralMethod.Classes
{
    public class RectangleMethod:ICalculator
    {
        /// <summary>
        /// Расчет интеграла методом прямоугольника
        /// </summary>
        public double Calculate(int count, double downLimit, double upLimit, out double time, Func<double, double> integral)
        {
            //проверка на некорректное число шагов
            if (count < 0)
            {
                throw new ArgumentException("Incorrect number of steps");
            }

            bool changeLimit = false;

            //перестановка пределов
            if (downLimit > upLimit)
            {
                double tempLimit = downLimit;
                downLimit = upLimit;
                upLimit = tempLimit;
                changeLimit = true;
            }

            Stopwatch stopwatch = new Stopwatch();
            TimeSpan timeSpan;

            double sum = 0;
            double h = (upLimit - downLimit) / count;

            stopwatch.Start();
            for (int i = 1; i <= count; i++)
            {
                sum += integral(downLimit + h * i - 0.5 * h);
            }

            stopwatch.Stop();

            timeSpan = stopwatch.Elapsed;
            time = timeSpan.TotalMilliseconds;

            if (changeLimit)
            {
                return -h * sum;
            }
            else
            {
                return h * sum;
            }
        }

        public double ParallelCalculate(int count, double downLimit, double upLimit, out double time, Func<double, double> integral)
        {
            //проверка на некорректное число шагов
            if (count < 0)
            {
                throw new ArgumentException("Incorrect number of steps");
            }

            bool changeLimit = false;

            //перестановка пределов
            if (downLimit > upLimit)
            {
                double tempLimit = downLimit;
                downLimit = upLimit;
                upLimit = tempLimit;
                changeLimit = true;
            }

            Stopwatch stopwatch = new Stopwatch();
            TimeSpan timeSpan;
            object locker = new object();

            double sum = 0;
            double h = (upLimit - downLimit) / count;

            stopwatch.Start();
            Parallel.For(0, count + 1,
                () => 0.0,
                (i, state, localTotal) => localTotal + integral(downLimit + h * i - 0.5 * h),
                localTotal => { lock (locker) sum += localTotal; });
            stopwatch.Stop();

            timeSpan = stopwatch.Elapsed;
            time = timeSpan.TotalMilliseconds;

            if (changeLimit)
            {
                return -h * sum;
            }
            else
            {
                return h * sum;
            }
        }
    }
}
