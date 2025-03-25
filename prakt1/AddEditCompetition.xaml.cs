using prakt1.Models;
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
using System.Windows.Shapes;

namespace prakt1
{
    /// <summary>
    /// Логика взаимодействия для AddEditCompetition.xaml
    /// </summary>
    public partial class AddEditCompetition : Window
    {
        private Competition _competition;
        private SpirtContext _context;
        public AddEditCompetition()
        {
            InitializeComponent();
            _context = new SpirtContext();
            Width += 20;
            Height += 20;
        }
        public AddEditCompetition(Competition competition) : this()
        {
            _competition = competition;
            NameCompetitionTextBox.Text = _competition.CompetitionName.ToString();
            DateCompetition.SelectedDate = _competition.CompetitionDate.ToDateTime(TimeOnly.MinValue);
            LocationCompetitionTextBox.Text = _competition.SportLocation.ToString();
            SportIdCompetitionTextBox.Text = _competition.SportId.ToString();
        }

        private void SportIdCompetitionTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !int.TryParse(e.Text, out _);
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (DateCompetition.SelectedDate == null ||
               string.IsNullOrEmpty(NameCompetitionTextBox.Text) ||
               string.IsNullOrEmpty(LocationCompetitionTextBox.Text) ||
               string.IsNullOrEmpty(SportIdCompetitionTextBox.Text))
            {
                MessageBox.Show("Заполните все поля!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!int.TryParse(SportIdCompetitionTextBox.Text, out int sportid) || sportid < 0)
            {
                MessageBox.Show("Значение кода вида спорта должно быть положительным числом!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            using (SpirtContext _context = new SpirtContext())
            {
                DateOnly competitionDate = DateOnly.FromDateTime(DateCompetition.SelectedDate.Value);

                if (_competition == null)
                {
                    _context.Competitions.Add(new Competition
                    {
                        CompetitionDate = competitionDate, // Используем DateOnly
                        CompetitionName = NameCompetitionTextBox.Text,
                        SportLocation = LocationCompetitionTextBox.Text,
                        SportId = int.Parse(SportIdCompetitionTextBox.Text)
                    });
                }
                else
                {
                    _competition.CompetitionDate = competitionDate; // Используем DateOnly
                    _competition.CompetitionName = NameCompetitionTextBox.Text;
                    _competition.SportLocation = LocationCompetitionTextBox.Text;
                    _competition.SportId = int.Parse(SportIdCompetitionTextBox.Text);
                    _context.Competitions.Update(_competition);
                }
                _context.SaveChanges();
            }
            this.Close();
        }
    }
}
