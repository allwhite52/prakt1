using prakt1.Models;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace prakt1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private SpirtContext _context;
        public MainWindow()
        {
            InitializeComponent();
            _context = new SpirtContext();
            LoadDBInDataGrid();
            Height += 20;
            Width += 20;
        }
        public void LoadDBInDataGrid()
        {
            using (SpirtContext _context = new SpirtContext())
            {
                SportView.ItemsSource = _context.Sports.ToList();
                AthletesView.ItemsSource = _context.Athletes.ToList();
                CompetitionView.ItemsSource = _context.Competitions.ToList();
                ParticipationView.ItemsSource = _context.Participations.ToList();
                Zapros1View.ItemsSource = _context.CompetitionPlaces.ToList();
                Zapros2View.ItemsSource = _context.AthletesInMultipleSports.ToList();
                Zapros3View.ItemsSource = _context.RecordBreakingAthletes.ToList();
                Zapros4View.ItemsSource = _context.Top3ResultsOfKaravaevInRunnings.ToList();
            }
        }

        private void AddButtonSport_Click(object sender, RoutedEventArgs e)
        {
            var addEditSport = new AddEditSport()
            {
                Title = "Добавить запись"
            };
            addEditSport.ShowDialog();
            LoadDBInDataGrid();
        }

        private void EditButtonSport_Click(object sender, RoutedEventArgs e)
        {
            if (SportView.SelectedItem is Sport selectedOrder)
            {
                var addEditSport = new AddEditSport(selectedOrder)
                {
                    Title = "Редактировать запись"
                };
                addEditSport.ShowDialog();
                LoadDBInDataGrid();
            }
        }

        private void DeleteButtonSport_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result;
            result = MessageBox.Show("Удалить запись?", "Удаление записи.", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    Sport row = (Sport)SportView.SelectedItem;
                    if (row != null)
                    {
                        using (SpirtContext _context = new SpirtContext())
                        {
                            _context.Sports.Remove(row);
                            _context.SaveChanges();
                        }
                        LoadDBInDataGrid();
                    }
                }
                catch
                {
                    MessageBox.Show("Ошибка.", "Ошибка удаления.", MessageBoxButton.OKCancel, MessageBoxImage.Error);
                }
            }
            else
            {
                SportView.Focus();
            }
        }

        private void SearchSportBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            using (var _context = new SpirtContext())
            {
                string searchItem = SearchSportBox.Text.ToLower();
                var filterSports = _context.Sports
                    .Where(a => (a.SportName != null && a.SportName.ToLower().Contains(searchItem)))
                    .ToList();
                SportView.ItemsSource = filterSports;
            }
        }

        private void AddButtonAthletes_Click(object sender, RoutedEventArgs e)
        {
            var addEditAthletes = new AddEditAthletes()
            {
                Title = "Добавить запись"
            };
            addEditAthletes.ShowDialog();
            LoadDBInDataGrid();
        }

        private void EditButtonAthletes_Click(object sender, RoutedEventArgs e)
        {
            if (AthletesView.SelectedItem is Athlete selectedOrder)
            {
                var addEditAthletes = new AddEditAthletes(selectedOrder)
                {
                    Title = "Редактировать запись"
                };
                addEditAthletes.ShowDialog();
                LoadDBInDataGrid();
            }
        }

        private void DeleteButtonAthletes_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result;
            result = MessageBox.Show("Удалить запись?", "Удаление записи.", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    Athlete row = (Athlete)AthletesView.SelectedItem;
                    if (row != null)
                    {
                        using (SpirtContext _context = new SpirtContext())
                        {
                            _context.Athletes.Remove(row);
                            _context.SaveChanges();
                        }
                        LoadDBInDataGrid();
                    }
                }
                catch
                {
                    MessageBox.Show("Ошибка.", "Ошибка удаления.", MessageBoxButton.OKCancel, MessageBoxImage.Error);
                }
            }
            else
            {
                AthletesView.Focus();
            }
        }

        private void SearchAthletesBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            using (var _context = new SpirtContext())
            {
                string searchItem = SearchAthletesBox.Text.ToLower();
                var filterAthletes = _context.Athletes
                    .Where(b => (b.FirstName != null && b.FirstName.ToLower().Contains(searchItem)) ||
                                (b.MiddleName != null && b.MiddleName.Contains(searchItem)) ||
                                (b.LastName != null && b.LastName.Contains(searchItem)) ||
                                (b.Team != null && b.Team.Contains(searchItem)) ||
                                (b.Category != null && b.Category.Contains(searchItem)))
                    .ToList();
                AthletesView.ItemsSource = filterAthletes;
            }
        }

        private void AddButtonCompetition_Click(object sender, RoutedEventArgs e)
        {
            var addEditCompetition = new AddEditCompetition()
            {
                Title = "Добавить запись"
            };
            addEditCompetition.ShowDialog();
            LoadDBInDataGrid();
        }

        private void EditButtonCompetition_Click(object sender, RoutedEventArgs e)
        {
            if (CompetitionView.SelectedItem is Competition selectedOrder)
            {
                var addEditCompetition = new AddEditCompetition(selectedOrder)
                {
                    Title = "Редактировать запись"
                };
                addEditCompetition.ShowDialog();
                LoadDBInDataGrid();
            }
        }

        private void DeleteButtonCompetition_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result;
            result = MessageBox.Show("Удалить запись?", "Удаление записи.", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    Competition row = (Competition)CompetitionView.SelectedItem;
                    if (row != null)
                    {
                        using (SpirtContext _context = new SpirtContext())
                        {
                            _context.Competitions.Remove(row);
                            _context.SaveChanges();
                        }
                        LoadDBInDataGrid();
                    }
                }
                catch
                {
                    MessageBox.Show("Ошибка.", "Ошибка удаления.", MessageBoxButton.OKCancel, MessageBoxImage.Error);
                }
            }
            else
            {
                CompetitionView.Focus();
            }
        }

        private void SearchCompetitionBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            using (var _context = new SpirtContext())
            {
                string searchItem = SearchCompetitionBox.Text.ToLower();
                var filterCompetition = _context.Competitions
                    .Where(c => (c.CompetitionName != null && c.CompetitionName.ToLower().Contains(searchItem)) ||
                                (c.SportLocation != null && c.SportLocation.Contains(searchItem)))
                    .ToList();
                AthletesView.ItemsSource = filterCompetition;
            }
        }

        private void AddButtonParticipation_Click(object sender, RoutedEventArgs e)
        {
            var addEditParticipation = new AddEditParticipation()
            {
                Title = "Добавить запись"
            };
            addEditParticipation.ShowDialog();
            LoadDBInDataGrid();
        }

        private void EditButtonParticipation_Click(object sender, RoutedEventArgs e)
        {
            if (ParticipationView.SelectedItem is Participation selectedOrder)
            {
                var addEditParticipation = new AddEditParticipation(selectedOrder)
                {
                    Title = "Редактировать запись"
                };
                addEditParticipation.ShowDialog();
                LoadDBInDataGrid();
            }
        }

        private void DeleteButtonParticipation_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result;
            result = MessageBox.Show("Удалить запись?", "Удаление записи.", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    Participation row = (Participation)CompetitionView.SelectedItem;
                    if (row != null)
                    {
                        using (SpirtContext _context = new SpirtContext())
                        {
                            _context.Participations.Remove(row);
                            _context.SaveChanges();
                        }
                        LoadDBInDataGrid();
                    }
                }
                catch
                {
                    MessageBox.Show("Ошибка.", "Ошибка удаления.", MessageBoxButton.OKCancel, MessageBoxImage.Error);
                }
            }
            else
            {
                ParticipationView.Focus();
            }
        }
    }
}