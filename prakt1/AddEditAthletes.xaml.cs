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
    /// Логика взаимодействия для AddEditAthletes.xaml
    /// </summary>
    public partial class AddEditAthletes : Window
    {
        private Athlete _athlete;
        private SpirtContext _context;
        public AddEditAthletes()
        {
            InitializeComponent();
            _context = new SpirtContext();
            Height += 20;
            Width += 20;
        }
        public AddEditAthletes(Athlete athlete) : this()
        {
            _athlete = athlete;
            FirstNameAthletesTextBox.Text = _athlete.FirstName.ToString();
            MiddleNameAthletesTextBox.Text = _athlete.MiddleName.ToString();
            LastNameAthletesTextBox.Text = _athlete.LastName.ToString();
            BirthYearAthletesTextBox.Text = _athlete.BirthYear.ToString();
            TeamAthletesTextBox.Text = _athlete.Team.ToString();
            CategoryAthletesTextBox.Text= _athlete.Category.ToString();
        }

        private void BirthYearAthletesTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !int.TryParse(e.Text, out _);
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(FirstNameAthletesTextBox.Text) ||
               string.IsNullOrEmpty(MiddleNameAthletesTextBox.Text) ||
               string.IsNullOrEmpty(LastNameAthletesTextBox.Text) ||
               string.IsNullOrEmpty(TeamAthletesTextBox.Text) ||
               string.IsNullOrEmpty(CategoryAthletesTextBox.Text) ||
               string.IsNullOrEmpty(BirthYearAthletesTextBox.Text))
            {
                MessageBox.Show("Заполните все поля!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!int.TryParse((BirthYearAthletesTextBox.Text), out int birth) || birth < 0)
            {
                MessageBox.Show("Год рождения должен быть положительным числом!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            using (SpirtContext _context = new SpirtContext())
            {
                if (_athlete == null)
                {
                    _context.Athletes.Add(new Athlete
                    {
                        FirstName = FirstNameAthletesTextBox.Text,
                        MiddleName = MiddleNameAthletesTextBox.Text,
                        LastName = LastNameAthletesTextBox.Text,
                        BirthYear = int.Parse(BirthYearAthletesTextBox.Text),
                        Team = TeamAthletesTextBox.Text,
                        Category = CategoryAthletesTextBox.Text
                    });
                }
                else
                {
                    _athlete.FirstName = FirstNameAthletesTextBox.Text;
                    _athlete.MiddleName = MiddleNameAthletesTextBox.Text;
                    _athlete.LastName = LastNameAthletesTextBox.Text;
                    _athlete.BirthYear = int.Parse(BirthYearAthletesTextBox.Text);
                    _athlete.Team = TeamAthletesTextBox.Text;
                    _athlete.Category = CategoryAthletesTextBox.Text;
                    _context.Athletes.Update(_athlete);
                }
                _context.SaveChanges();
            }
            this.Close();
        }
    }
}
