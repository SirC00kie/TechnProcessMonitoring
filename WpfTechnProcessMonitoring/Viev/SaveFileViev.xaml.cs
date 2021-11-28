using System;
using System.IO;
using System.Windows;
using TechnProcessMonitoring.BL.Controller;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace WpfTechnProcessMonitoring.Viev
{
    /// <summary>
    /// Логика взаимодействия для SaveFileViev.xaml
    /// </summary>
    public partial class SaveFileViev : Window
    {
        readonly DataTechnProcessController _experiment = new DataTechnProcessController();
        private bool _check;
        private Chart _chart;
        private readonly string _directoryImage = "..\\..\\..\\Image";


        public SaveFileViev(bool check)
        {
            InitializeComponent();
            _check = check;

        }
        public SaveFileViev(bool check, Chart chart)
        {
            InitializeComponent();
            _check = check;
            _chart = chart;

        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (_check)
            {
                _experiment.Save(PathTextBox.Text);
            }
            else
            {
                var directory = Directory.CreateDirectory(_directoryImage);
                _chart.SaveImage(directory.FullName + $"\\{PathTextBox.Text}.png", ChartImageFormat.Png);
                _experiment.CreateReport(PathTextBox.Text);
            }
            Close();
        }
    }
}
