using MathIntegralMethod.Classes;
using System;
using Xunit;


namespace MathIntegralMethodTest
{
    public class MathIntegralTest
    {
        [Fact]
        public void Integral_X_X_Rectangle_Succces()
        {
            //Arrange
            double xStart = 0.0;
            double xEnd = 100.0;
            Func<double, double> func = x => x * x;
            int steps = 100000;
            double time;
            double result;
            double expected = 333333.333333;
            ICalculator calculator = new RectangleMethod();

            //Acl
            result = calculator.Calculate(steps, xStart, xEnd, out time, func);

            //Assert
            Assert.Equal(expected, result, 4);
        }

        [Fact]
        public void ParallelCalculateTrapeze_X_X_EqualCalculate()
        {
            //Arrange
            double xStart = 0.0;
            double xEnd = 100.0;
            Func<double, double> func = x => x * x;
            int steps = 100000;
            double time;
            double result;
            double expected;
            ICalculator calculator = new TrapezeMethod();

            //Acl
            result = calculator.ParallelCalculate(steps, xStart, xEnd, out time, func);
            expected = calculator.Calculate(steps, xStart, xEnd, out time, func);

            //Assert
            Assert.Equal(expected, result, 6);
        }

        [Fact]
        public void Integral_X_X_Trapeze_Succces()
        {
            //Arrange
            double xStart = 0.0;
            double xEnd = 100.0;
            Func<double, double> func = x => x * x;
            int steps = 100000;
            double time;
            double result;
            double expected = 333333.333333;
            ICalculator calculator = new TrapezeMethod();

            //Acl
            result = calculator.Calculate(steps, xStart, xEnd, out time, func);

            //Assert
            Assert.Equal(expected, result, 4);
        }

        [Fact]
        public void ParallelCalculateRectangle_X_X_EqualCalculate()
        {
            //Arrange
            double xStart = 0.0;
            double xEnd = 100.0;
            Func<double, double> func = x => x * x;
            int steps = 100000;
            double time;
            double result;
            double expected;
            ICalculator calculator = new RectangleMethod();

            //Acl
            result = calculator.ParallelCalculate(steps, xStart, xEnd, out time, func);
            expected = calculator.Calculate(steps, xStart, xEnd, out time, func);

            //Assert
            Assert.Equal(expected, result, 6);
        }

        [Fact]
        public void CalculateTrapeze_NegativeSteps_ArgumentException()
        {
            //Arrange
            double xStart = 0.0;
            double xEnd = 100.0;
            Func<double, double> func = x => x * x;
            int steps = -100000;
            double time;
            ICalculator calculator = new TrapezeMethod();

            //Acl

            //Assert
            Assert.Throws<ArgumentException>(() => calculator.Calculate(steps, xStart, xEnd, out time, func));
        }

        [Fact]
        public void CalculateRectangle_NegativeSteps_ArgumentException()
        {
            //Arrange
            double xStart = 0.0;
            double xEnd = 100.0;
            Func<double, double> func = x => x * x;
            int steps = -100000;
            double time;
            ICalculator calculator = new RectangleMethod();

            //Acl

            //Assert
            Assert.Throws<ArgumentException>(() => calculator.Calculate(steps, xStart, xEnd, out time, func));
        }

        [Fact]
        public void CalculatorTrapeze_ConfuseTheLimits_CorrectSolution()
        {
            //Arrange
            double xStart = 100.0;
            double xEnd = 0.0;
            Func<double, double> func = x => x * x;
            int steps = 100000;
            double time;
            double result;
            double expected = -333333.333333;
            ICalculator calculator = new TrapezeMethod();

            //Acl
            result = calculator.Calculate(steps, xStart, xEnd, out time, func);

            //Assert
            Assert.Equal(expected, result, 4);
        }

        [Fact]
        public void CalculatorRectangle_ConfuseTheLimits_CorrectSolution()
        {
            //Arrange
            double xStart = 100.0;
            double xEnd = 0.0;
            Func<double, double> func = x => x * x;
            int steps = 100000;
            double time;
            double result;
            double expected = -333333.333333;
            ICalculator calculator = new RectangleMethod();

            //Acl
            result = calculator.Calculate(steps, xStart, xEnd, out time, func);

            //Assert
            Assert.Equal(expected, result, 4);
        }
    }
}