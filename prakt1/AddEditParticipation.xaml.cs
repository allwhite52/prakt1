using Microsoft.IdentityModel.Tokens;
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
    /// Логика взаимодействия для AddEditParticipation.xaml
    /// </summary>
    public partial class AddEditParticipation : Window
    {
        private Participation _participation;
        private SpirtContext _context;
        public AddEditParticipation()
        {
            InitializeComponent();
            _context = new SpirtContext();
            Width += 20;
            Height += 20;
        }
        public AddEditParticipation(Participation participation) : this()
        {
            _participation = participation;
            SportIdParticipationTextBox.Text = _participation.SportId.ToString();
            AthleteIdParticipationTextBox.Text = _participation.Athlete.ToString();
            CompetitionIdParticipationTextBox.Text = _participation.CompetitionId.ToString();
            ResultParticipationTextBox.Text = _participation.Result.ToString();
            PlaceParticipationTextBox.Text = _participation.Place.ToString();
        }

        private void SportIdParticipationTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !int.TryParse(e.Text, out _);
        }

        private void AthleteIdParticipationTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !int.TryParse(e.Text, out _);
        }

        private void CompetitionIdParticipationTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !int.TryParse(e.Text, out _);
        }

        private void ResultParticipationTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !decimal.TryParse(e.Text, out _);
        }

        private void PlaceParticipationTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !int.TryParse(e.Text, out _);
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(SportIdParticipationTextBox.Text) ||
               string.IsNullOrEmpty(AthleteIdParticipationTextBox.Text) ||
               string.IsNullOrEmpty(ResultParticipationTextBox.Text) ||
               string.IsNullOrEmpty(PlaceParticipationTextBox.Text) ||
               string.IsNullOrEmpty(CompetitionIdParticipationTextBox.Text))
            {
                MessageBox.Show("Заполните все поля!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!int.TryParse(SportIdParticipationTextBox.Text, out int sportid) || sportid < 0)
            {
                MessageBox.Show("Значение кода вида спорта должно быть положительным числом!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!int.TryParse(AthleteIdParticipationTextBox.Text, out int athleteid) || sportid < 0)
            {
                MessageBox.Show("Значение кода вида спорта должно быть положительным числом!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!int.TryParse(ResultParticipationTextBox.Text, out int result) || sportid < 0)
            {
                MessageBox.Show("Значение кода вида спорта должно быть положительным числом!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!int.TryParse(PlaceParticipationTextBox.Text, out int place) || sportid < 0)
            {
                MessageBox.Show("Значение кода вида спорта должно быть положительным числом!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!int.TryParse(CompetitionIdParticipationTextBox.Text, out int competitionid) || sportid < 0)
            {
                MessageBox.Show("Значение кода вида спорта должно быть положительным числом!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            using (SpirtContext _context = new SpirtContext())
            {
                if (_participation == null)
                {
                    _context.Participations.Add(new Participation
                    {
                        SportId = int.Parse(SportIdParticipationTextBox.Text),
                        AthleteId = int.Parse(AthleteIdParticipationTextBox.Text),
                        CompetitionId = int.Parse(CompetitionIdParticipationTextBox.Text),
                        Place = int.Parse(PlaceParticipationTextBox.Text),
                        Result = decimal.Parse(ResultParticipationTextBox.Text)
                    });
                }
                else
                {
                    _participation.SportId = int.Parse(SportIdParticipationTextBox.Text);
                    _participation.AthleteId = int.Parse(AthleteIdParticipationTextBox.Text);
                    _participation.CompetitionId = int.Parse(CompetitionIdParticipationTextBox.Text);
                    _participation.Place = int.Parse(PlaceParticipationTextBox.Text);
                    _participation.Result = decimal.Parse(ResultParticipationTextBox.Text);
                    _context.Participations.Update(_participation);
                }
                _context.SaveChanges();
            }
            this.Close();
        }
    }
}
