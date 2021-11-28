using LiveCharts.Wpf.Charts.Base;
using System;
using System.IO;
using System.Windows;
using TechnProcessMonitoring.BL.Controller;

namespace WpfTechnProcessMonitoring.Viev
{
    /// <summary>
    /// Логика взаимодействия для SaveFileViev.xaml
    /// </summary>
    public partial class SaveFileViev : Window
    {
        DataTechnProcessController _experiment = new DataTechnProcessController();
        private bool _check;
        private Chart _chart;
        private readonly string _directoryImage = "..\\..\\..\\Image";

        public SaveFileViev(bool check, Chart chart)
        {
            InitializeComponent();
            check = _check;
            chart = _chart;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (_check)
            {
                _experiment.Save(PathTextBox.Text);
            }
            else
            {

            }
            Close();
        }
    }
}
