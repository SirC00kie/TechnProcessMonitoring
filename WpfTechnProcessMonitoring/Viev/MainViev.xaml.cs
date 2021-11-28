using System.Windows;
using TechnProcessMonitoring.BL.Controller;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms.DataVisualization.Charting;
using Microsoft.Win32;

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
            MainChart.ChartAreas.Add(new ChartArea("main"));

            var pressureSeries = new Series(_pressure){ IsValueShownAsLabel = true };
            var concentrationSeries = new Series(_concentration) { IsValueShownAsLabel = true };
            pressureSeries.ChartType = SeriesChartType.Line;
            concentrationSeries.ChartType = SeriesChartType.Line;
            MainChart.Series.Add(pressureSeries);
            MainChart.Series.Add(concentrationSeries);
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
            if (((int)val[0] < DataTechnProcessController.DataResults.Count) && ((int)val[0]>0))
            {
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
            MainChart.Series[_pressure].Points.Clear();
            MainChart.Series[_concentration].Points.Clear();
            var result = DataTechnProcessController.DataResults.OrderBy(x => x.Consumption).ToList();
            if (result.Count > 0)
            {
                for (int i = 0; i < result.Count; i++)
                {
                    MainChart.Series[_pressure].Points.AddXY(result[i].Consumption, result[i].Pressure);
                    MainChart.Series[_concentration].Points.AddXY(result[i].Consumption, result[i].Concentration);
                }
            }
            else
            {
                MessageBox.Show("Нет данных эксперимента", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataTechnProcessController.DataResults.Count > 0)
            {
                MainChart.Series[_pressure].Points.Clear();
                MainChart.Series[_concentration].Points.Clear();
                DataTechnProcessController.DataResults.Clear();
                DataViev.ItemsSource = DataTechnProcessController.DataResults.OrderBy(x => x.Consumption).ToList();
            }
            else
            {
                MessageBox.Show("Нет данных которые надо очистить", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
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
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                string filename = openFileDialog.FileName;
                bool IsTrue;
                IsTrue = _experiment.Open(filename);
                if (IsTrue)
                {
                    DataViev.ItemsSource = DataTechnProcessController.DataResults.OrderBy(x => x.Consumption).ToList();

                }
                else
                {
                    DataTechnProcessController.DataResults.Clear();
                    DataViev.ItemsSource = DataTechnProcessController.DataResults.ToList();
                    MessageBox.Show("Не удалось прочитать файл", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            _check = true;
            SaveFileViev Form = new SaveFileViev(_check);
            Form.ShowDialog();
        }

        private void CreateReportButton_Click(object sender, RoutedEventArgs e)
        {
            _check = false;
            GraphicButton_Click(sender, e);
            SaveFileViev Form = new SaveFileViev(_check, MainChart);
            Form.Show();
        }

        private void OpenReportButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                string filename = openFileDialog.FileName;
                _experiment.OpenReport(filename);
            }
        }

        private void InfoButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AboutButton_Click(object sender, RoutedEventArgs e)
        {
            AboutViev aboutViev = new AboutViev();
            aboutViev.Show();
        }

        
    }
}
