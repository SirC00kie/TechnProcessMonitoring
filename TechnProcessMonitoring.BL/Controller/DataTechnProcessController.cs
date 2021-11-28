using System;
using TechnProcessMonitoring.BL.Model;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Microsoft.Office.Interop.Word;
using System.Diagnostics;

namespace TechnProcessMonitoring.BL.Controller
{
    public class DataTechnProcessController
    {
        private string _directory_result = "..\\..\\..\\Results";
        private string _directoryReport = "..\\..\\..\\Reports";
        private string _directoryImage = "..\\..\\..\\Image";

        public static List<DataTechnProcess> DataResults = new List<DataTechnProcess>();
        public DataTechnProcess DataTechnProcess { get; }

        public DataTechnProcessController(){}

        public DataTechnProcessController(double consumption, double pressure, double concentration)
        {
            DataTechnProcess = new DataTechnProcess(consumption, pressure, concentration);
            DataResults.Add(DataTechnProcess);
        }

        public void Save(string fileName)
        {
            var directory = Directory.CreateDirectory(_directory_result);
            var formatter = new BinaryFormatter();

            using (FileStream fs = new FileStream(directory.FullName + $"\\ {fileName}.dat", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs,DataResults);
            }
        }

        public void Open(string fileName)
        {
            DataResults.Clear();
            var formatter = new BinaryFormatter();
            
            using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                if (fs.Length > 0)
                {
                    DataResults = (List<DataTechnProcess>)formatter.Deserialize(fs);
                }
            }
        }

        public void CreateReport(string fileName)
        {
            var directory = Directory.CreateDirectory(_directoryReport);
            var dir = Directory.CreateDirectory(_directoryImage);
            string NameofFile = directory.FullName + "\\" + $"{fileName}.docx";
            var word = new Application();

            var document = word.Documents.Add();


            Paragraph paragraph1 = document.Content.Paragraphs.Add();
            string styleHeading1 = "Обычный";
            paragraph1.Range.set_Style(styleHeading1);
            paragraph1.Range.Font.Size = 16;
            paragraph1.Range.Font.Name = "Times New Roman";
            paragraph1.Range.Text = $"Отчёт по результатам эксперимента за {DateTime.Now}";
            paragraph1.Range.InsertParagraphAfter();


            Paragraph paragraph = document.Content.Paragraphs.Add();
            string styleHeading = "Обычный";
            paragraph.Range.set_Style(styleHeading);
            paragraph.Range.Font.Size = 16;
            paragraph.Range.Font.Name = "Times New Roman";
            paragraph.Range.Text = $"График";
            paragraph.Range.InsertParagraphAfter();
            document.Shapes.AddPicture(dir.FullName + "\\" + $"{fileName}.png", Top: 100);
            document.Words.Last.InsertBreak(WdBreakType.wdPageBreak);

            CreateTable(document);

            document.SaveAs(NameofFile);
            word.Quit();
        }

        private void CreateTable(Document document)
        {
            Paragraph paragraph = document.Content.Paragraphs.Add();
            string styleHeading = "Обычный";
            paragraph.Range.set_Style(styleHeading);
            paragraph.Range.Font.Size = 16;
            paragraph.Range.Font.Name = "Times New Roman";
            paragraph.Range.Text = $"Таблица экспериментальных данных";
            paragraph.Range.InsertParagraphAfter();

            var table = document.Tables.Add(paragraph.Range, DataResults.Count + 1, 3);
            table.Borders.Enable = 1;

            table.Rows[1].Cells[1].Range.Text = "Расход, кг/сек";
            table.Rows[1].Cells[2].Range.Text = "Отклонение уровня, мм";
            table.Rows[1].Cells[3].Range.Text = "Температура, С";

            table.Rows[1].Range.Font.Name = "Times New Roman";
            table.Rows[1].Range.Font.Size = 10;
            table.Rows[1].Shading.BackgroundPatternColor = WdColor.wdColorGray25;

            table.Rows[1].Cells.VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;
            table.Rows[1].Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;

            for (int i = 1; i < DataResults.Count + 1; i++)
            {
                table.Rows[i + 1].Cells[1].Range.Text = DataResults[i - 1].Consumption.ToString();
                table.Rows[i + 1].Cells[2].Range.Text = DataResults[i - 1].Pressure.ToString();
                table.Rows[i + 1].Cells[3].Range.Text = DataResults[i - 1].Concentration.ToString();
            }
        }
        public void OpenReport(string filename)
        {
            Process.Start(filename);
        }
        public void Change(double[] value)
        {
            DataResults[Convert.ToInt32(value[0] - 1)].Pressure = value[1];
            DataResults[Convert.ToInt32(value[0] - 1)].Concentration = value[2];
        }
    }
}