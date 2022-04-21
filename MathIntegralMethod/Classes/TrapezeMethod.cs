using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MathIntegralMethod.Classes
{
    public class TrapezeMethod:ICalculator
    {
        /// <summary>
        /// Расчет интеграла методом трапеций
        /// </summary>
        public double Calculate(int count, double downLimit, double upLimit, out double time, Func<double, double> integral)
        {
            //проверка на некорректное число шагов
            if (count < 0)
            {
                throw new ArgumentException("Incorrect number of steps");
            }

            //перестановка пределов
            if (downLimit > upLimit)
            {
                double tempLimit = downLimit;
                downLimit = upLimit;
                upLimit = tempLimit;
            }

            Stopwatch stopwatch = new Stopwatch();
            TimeSpan timeSpan;

            double sum = 0;
            double h = (upLimit - downLimit) / count;

            stopwatch.Start();
            for (int i = 0; i < count; i++)
            {
                sum += integral(downLimit + h * i);
            }
            sum += (integral(upLimit) + integral(downLimit)) / 2;
            stopwatch.Stop();

            timeSpan = stopwatch.Elapsed;
            time = timeSpan.TotalMilliseconds;

            return h * sum;
        }

        /// <summary>
        /// Расчет интеграла методом трапеций параллельно
        /// </summary>
        public double ParallelCalculate(int count, double downLimit, double upLimit, out double time, Func<double, double> integral)
        {
            //проверка на некорректное число шагов
            if (count < 0)
            {
                throw new ArgumentException("Incorrect number of steps");
            }

            //перестановка пределов
            if (downLimit > upLimit)
            {
                double tempLimit = downLimit;
                downLimit = upLimit;
                upLimit = tempLimit;
            }

            Stopwatch stopwatch = new Stopwatch();
            TimeSpan timeSpan;

            double sum = 0;
            double h = (upLimit - downLimit) / count;
            object locker = new object();

            stopwatch.Start();
            Parallel.For(0, count,
                () => 0.0,
                (i, state, localTotal) => localTotal + integral(downLimit + h * i),
                localTotal => { lock (locker) sum += localTotal; });
            sum += (integral(upLimit) + integral(downLimit)) / 2;
            stopwatch.Stop();

            timeSpan = stopwatch.Elapsed;
            time = timeSpan.TotalMilliseconds;

            return h * sum;
        }
    }
}
