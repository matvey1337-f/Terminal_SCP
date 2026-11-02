using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Terminal_SCP
{
    public partial class MainWindow : Window
    {
        private readonly string _role;

        public MainWindow(string login, string role)
        {
            InitializeComponent();
            _role = role;

            lblAgent.Text = string.Format("АГЕНТ: {0} │ ДОПУСК: {1}",
                string.IsNullOrWhiteSpace(login) ? "AGENT" : login.ToUpper(),
                role == "Admin" ? "УРОВЕНЬ 4 (АДМИНИСТРАТОР)" : "УРОВЕНЬ 2");

            ApplyRole();
            LoadDemoData();
        }

        private void ApplyRole()
        {
            bool isAdmin = _role == "Admin";
            btnAdd.Visibility = isAdmin ? Visibility.Visible : Visibility.Collapsed;
            btnDelete.Visibility = isAdmin ? Visibility.Visible : Visibility.Collapsed;

            if (isAdmin)
            {
                foreach (var b in FindVisualChildren<Border>(lvAnomalies))
                {
                    if (b.Name == "SecretRow")
                        b.Visibility = Visibility.Visible;
                }
            }
        }

        private void LoadDemoData()
        {
            var list = new List<AnomalyCardVm>
            {
                new AnomalyCardVm("SCP-173", "Euclid", 3, "Изолирован", new DateTime(2007, 1, 1),
                    "Скульптура, движется при отсутствии зрительного контакта", "Зона-19, сектор 4"),

                new AnomalyCardVm("SCP-096", "Euclid", 3, "Изолирован", new DateTime(2008, 5, 12),
                    "Гуманоид, реагирует на изображение лица", "Зона-██, камера 7"),

                new AnomalyCardVm("SCP-682", "Keter", 5, "Наблюдение", new DateTime(2006, 9, 3),
                    "Рептилия с высокой регенерацией", "Зона-19, бункер A"),

                new AnomalyCardVm("SCP-049", "Euclid", 3, "Изолирован", new DateTime(2007, 11, 20),
                    "«Чумной доктор»", "Зона-██"),

                new AnomalyCardVm("SCP-999", "Safe", 1, "Изолирован", new DateTime(2009, 2, 14),
                    "Желеобразное существо, дружелюбно", "Зона-19, крыло B"),

                new AnomalyCardVm("SCP-106", "Keter", 5, "Изолирован", new DateTime(2007, 4, 8),
                    "Старик, проходящий сквозь материю", "Зона-██"),

                new AnomalyCardVm("SCP-035", "Keter", 4, "Наблюдение", new DateTime(2008, 8, 1),
                    "Маска, обладающая личностью", "Зона-██"),

                new AnomalyCardVm("Артефакт «Красный диск»", "Бюро", 2, "Изолирован", new DateTime(2010, 3, 22),
                    "Диск, вызывающий головную боль", "Хранилище Бюро, ячейка 12"),

                new AnomalyCardVm("Артефакт «Стеклянный глаз»", "Бюро", 2, "Наблюдение", new DateTime(2011, 7, 30),
                    "Показывает воспоминания владельца", "Хранилище Бюро, ячейка 5"),

                new AnomalyCardVm("SCP-087", "Euclid", 3, "Изолирован", new DateTime(2009, 10, 10),
                    "Лестница с сущностью SCP-087-1", "Зона-██")
            };

            lvAnomalies.ItemsSource = list;
            lblCount.Text = "ОБЪЕКТОВ: " + list.Count;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new AddAnomalyWindow { Owner = this };
            dlg.ShowDialog();
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            new LoginWindow().Show();
            Close();
        }

        private static IEnumerable<T> FindVisualChildren<T>(DependencyObject p) where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(p); i++)
            {
                var c = VisualTreeHelper.GetChild(p, i);
                if (c is T t) yield return t;

                foreach (var x in FindVisualChildren<T>(c))
                    yield return x;
            }
        }
    }

    public class AnomalyCardVm
    {
        public string Title { get; }
        public string Category { get; }
        public string ThreatStars { get; }
        public string ContainmentStatus { get; }
        public DateTime Date { get; }
        public string Description { get; }
        public string SecretLocation { get; }

        public AnomalyCardVm(string title, string category, int threat, string status,
                             DateTime date, string description, string location)
        {
            Title = title;
            Category = category;
            ThreatStars = new string('▲', threat);
            ContainmentStatus = status;
            Date = date;
            Description = description;
            SecretLocation = location;
        }
    }
}