using prakt1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace prakt1
{
    /// <summary>
    /// Логика взаимодействия для AddEditSport.xaml
    /// </summary>
    public partial class AddEditSport : Window
    {
        private Sport _sport;
        private SpirtContext _context;
        public AddEditSport()
        {
            InitializeComponent();
            _context = new SpirtContext();
            Height += 20;
            Width += 20;
        }
        public AddEditSport(Sport sport) : this()
        {
            _sport = sport;
            SportDatePicker.SelectedDate = _sport.WorldRecordDate.ToDateTime(TimeOnly.MinValue);
            NameSportTextBox.Text = _sport.SportName.ToString();
            UnitSportTextBox.Text = _sport.UnitOfMeasurement.ToString();
            RecordSportTextBox.Text = _sport.WorldRecord.ToString();
        }

        private void RecordSportTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !decimal.TryParse(e.Text, out _);
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (SportDatePicker.SelectedDate == null ||
               string.IsNullOrEmpty(NameSportTextBox.Text) ||
               string.IsNullOrEmpty(RecordSportTextBox.Text) ||
               string.IsNullOrEmpty(UnitSportTextBox.Text))
            {
                MessageBox.Show("Заполните все поля!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!decimal.TryParse(RecordSportTextBox.Text, out decimal record) || record < 0)
            {
                MessageBox.Show("Значение рекорда должно быть положительным числом!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            using (SpirtContext _context = new SpirtContext())
            {
                // Преобразуем DateTime в DateOnly
                DateOnly sportDate = DateOnly.FromDateTime(SportDatePicker.SelectedDate.Value);

                if (_sport == null)
                {
                    _context.Sports.Add(new Sport
                    {
                        WorldRecordDate = sportDate, // Используем DateOnly
                        SportName = NameSportTextBox.Text,
                        UnitOfMeasurement = UnitSportTextBox.Text,
                        WorldRecord = decimal.Parse(RecordSportTextBox.Text)
                    });
                }
                else
                {
                    _sport.WorldRecordDate = sportDate; // Используем DateOnly
                    _sport.SportName = NameSportTextBox.Text;
                    _sport.UnitOfMeasurement = UnitSportTextBox.Text;
                    _sport.WorldRecord = decimal.Parse(RecordSportTextBox.Text);
                    _context.Sports.Update(_sport);
                }
                _context.SaveChanges();
            }
            this.Close();
        }
    }
}
