using System.Windows;

namespace Terminal_SCP
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string role = txtLogin.Text.Trim().ToLower() == "admin" ? "Admin" : "User";
            new MainWindow(txtLogin.Text.Trim(), role).Show();
            Close();
        }
    }
}