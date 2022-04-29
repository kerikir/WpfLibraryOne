using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MathIntegralMethod.Classes;
using OxyPlot;
using OxyPlot.Series;


namespace WpfLibraryOne
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Обработка нажатия кнопки расчета интеграла
        /// </summary>
        private void buttonCalculate_Click(object sender, RoutedEventArgs e)
        {
            DoCalculate();
        }

        /// <summary>
        /// Обработка выбора метода расчета интеграла
        /// </summary>
        private ICalculator GetCalculator()
        {
            switch (method.SelectedIndex)
            {
                case 0:
                    return new RectangleMethod();
                case 1:
                    return new TrapezeMethod();
                default:
                    throw new NotImplementedException();
            }
        }


        /// <summary>
        /// Расчет интеграла
        /// </summary>
        private void DoCalculate()
        {
            //считывание ввода пользователя
            int valueN = Convert.ToInt32(tbN.Text);
            double downLimit = Convert.ToDouble(tbDownLimit.Text);
            double upLimit = Convert.ToDouble(tbUpLimit.Text);

            ICalculator calculator = GetCalculator();
            double time;
            double result;

            //вычисление результата
            if (checkboxParallel.IsChecked == true) 
            {
                result = calculator.ParallelCalculate(valueN, downLimit, upLimit, out time, x => 2 * x - Math.Log(2 * x) + 234);
            }
            else
            {
                result = calculator.Calculate(valueN, downLimit, upLimit, out time, x => 2 * x - Math.Log(2 * x) + 234);
            }

            MessageBox.Show("Результат = " + result.ToString() + "\nВремя = " + time.ToString() + " мс");
        }

        /// <summary>
        /// Отрисовка графика
        /// </summary>
        private void GraphicDraw()
        {
            PlotModel plotModel = new PlotModel()
            {
                Title = "2x - ln(2x) + 234"
            };
            LineSeries lineSeries = new LineSeries();


            double downLimit = Convert.ToDouble(tbDownLimit.Text);
            double upLimit = Convert.ToDouble(tbUpLimit.Text);
            ICalculator calculator = GetCalculator();

            int totalSteps = 10000;
            for (int i = 1; i < totalSteps; i++)
            {
                double time = 0.0;
                double result = calculator.Calculate(i, downLimit, upLimit, out time, x => 2 * x - Math.Log(2 * x) + 234);
                lineSeries.Points.Add(new DataPoint(i, time));
            }

            plotModel.Series.Add(lineSeries);
            graphicOxyPlot.Model = plotModel;
        }

        /// <summary>
        /// Обработка кнопки - отрисовка графика
        /// </summary>
        private void buttonDrawGraphic_Click(object sender, RoutedEventArgs e)
        {
            GraphicDraw();
        }
    }
}