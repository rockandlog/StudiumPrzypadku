using System.Net.Http;
using System.Net.Http.Json;
using System.Windows;
using System.Windows.Controls;

namespace Zasobowo.Client
{
    public partial class RegisterWindow : Window
    {
        public RegisterWindow()
        {
            InitializeComponent();
        }

        private async void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            var email = EmailBox.Text.Trim();
            var password = PasswordBox.Password.Trim();
            var firstName = FirstNameBox.Text.Trim();
            var lastName = LastNameBox.Text.Trim();
            var role = (RoleBox.SelectedItem as ComboBoxItem)?.Content.ToString();

            if (string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(password) ||
                string.IsNullOrWhiteSpace(firstName) ||
                string.IsNullOrWhiteSpace(lastName) ||
                string.IsNullOrWhiteSpace(role))
            {
                ShowError("Uzupełnij wszystkie pola.");
                return;
            }

            var registerRequest = new
            {
                Email = email,
                Password = password,
                FirstName = firstName,
                LastName = lastName,
                Role = role
            };

            using var http = new HttpClient();
            try
            {
                var response = await http.PostAsJsonAsync("https://localhost:7031/api/auth/register", registerRequest);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Rejestracja zakończona sukcesem!", "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.DialogResult = true; // WAŻNE!
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Conflict)
                {
                    ShowError("Użytkownik o podanym emailu już istnieje.");
                }
                else
                {
                    ShowError("Błąd rejestracji. Spróbuj ponownie.");
                }
            }
            catch
            {
                ShowError("Brak połączenia z serwerem.");
            }
        }

        private void ShowError(string message)
        {
            ErrorText.Text = message;
            ErrorText.Visibility = Visibility.Visible;
        }

        private void LoginLink_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false; // po prostu zamknij
        }
    }
}
