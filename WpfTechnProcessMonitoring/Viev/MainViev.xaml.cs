using System.Windows;
using TechnProcessMonitoring.BL.Controller;
using TechnProcessMonitoring.BL.Model;
using System.Diagnostics;
using System.Linq;
using LiveCharts;
using LiveCharts.Wpf;
using LiveCharts.Defaults;
using System.Collections.Generic;

namespace WpfTechnProcessMonitoring.Viev
{
    /// <summary>
    /// Логика взаимодействия для MainViev.xaml
    /// </summary>
    public partial class MainViev : Window
    {
        private bool _check;
        private const string _pressure = "Давление";
        private const string _concentration = "Концентрация";
        private readonly DataTechnProcessController _experiment = new DataTechnProcessController();
        public MainViev()
        {
            InitializeComponent();
        }

        private void Clear()
        {
            TextBoxConcentration.Clear();
            TextBoxConsumption.Clear();
            TextBoxPressure.Clear();
        }
        
        private double[] Validation()
        {
            if (!string.IsNullOrEmpty(TextBoxConcentration.Text)
                && !string.IsNullOrWhiteSpace(TextBoxConcentration.Text)
                && !string.IsNullOrEmpty(TextBoxConsumption.Text)
                && !string.IsNullOrWhiteSpace(TextBoxConsumption.Text)
                && !string.IsNullOrEmpty(TextBoxPressure.Text)
                && !string.IsNullOrWhiteSpace(TextBoxPressure.Text)
                && double.TryParse(TextBoxConcentration.Text, out double Concentration)
                && double.TryParse(TextBoxConsumption.Text, out double Consuption)
                && double.TryParse(TextBoxPressure.Text, out double Pressure))
            {
                return new double[3] { Consuption, Pressure, Consuption };
            }
            else
            {
                return null;
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var val = Validation();
            if (val != null)
            {
                if (DataTechnProcessController.DataResults.Count == 0 && val[0] != 0)
                {
                    MessageBox.Show("Расход должен быть равен 0", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    DataTechnProcessController controller = new DataTechnProcessController(val[0], val[1], val[2]);
                    DataViev.ItemsSource = DataTechnProcessController.DataResults.OrderBy(x=>x.Concentration).ToList();
                    Clear();
                }
            }
            else
            {
                MessageBox.Show("Некорректный ввод данных", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ChangeButton_Click(object sender, RoutedEventArgs e)
        {
            var val = Validation();
            if (val != null)
            {
                var result = DataTechnProcessController.DataResults[(int)val[0]];
                if (result != null)
                {
                    val[0]++;
                    _experiment.Change(val);
                    DataViev.ItemsSource = DataTechnProcessController.DataResults.OrderBy(x => x.Consumption).ToList();
                }
                else
                {
                    MessageBox.Show("Некорректный ввод данных", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Некорректный ввод данных", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var val = Validation();
            if (val != null)
            {
                var result = DataTechnProcessController.DataResults.FirstOrDefault(exp => exp.Consumption == val[0]);
                if (result != null)
                {
                    DataTechnProcessController.DataResults.Remove(result);
                    DataViev.ItemsSource = DataTechnProcessController.DataResults.OrderBy(x => x.Consumption).ToList();
                    Clear();
                }
                else
                {
                    MessageBox.Show("Некорректный ввод данных", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Некорректный ввод данных", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void GraphicButton_Click(object sender, RoutedEventArgs e)
        {
            // LiveChart.Series.Clear();
            // var result = DataTechnProcessController.DataResults.OrderBy(x => x.Consumption).ToList();
            // result = new List<DataTechnProcess>();
            // SeriesCollection seriescollection = new SeriesCollection { };
            //
            // for (int i = 0; i < result.Count; i++)
            // {
            //     seriescollection.Add({ Title = "Расход", ChartValues = new ChartValues<double>(result)};
            // }
        }
        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DataViev_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            int rowIndex = DataViev.SelectedIndex;
            var result = DataTechnProcessController.DataResults[rowIndex];
            TextBoxConsumption.Text = result.Consumption.ToString();
            TextBoxPressure.Text = result.Pressure.ToString();
            TextBoxConcentration.Text = result.Concentration.ToString();
        }

        private void QuitButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void OpenButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CreateReportButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SaveReportButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void InfoButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AboutButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
