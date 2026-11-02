using System.Windows;

namespace Terminal_SCP
{
    public partial class AddAnomalyWindow : Window
    {
        public AddAnomalyWindow()
        {
            InitializeComponent();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}