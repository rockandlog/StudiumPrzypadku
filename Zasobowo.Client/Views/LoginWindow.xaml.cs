using System.Net.Http;
using System.Net.Http.Json;
using System.Windows;
using Zasobowo.Client.Models;

namespace Zasobowo.Client
{
    public partial class LoginWindow : Window
    {
        private readonly HttpClient _httpClient;

        public LoginWindow()
        {
            InitializeComponent();
            _httpClient = new HttpClient
            {
                BaseAddress = new System.Uri("https://localhost:7031")
            };
        }

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            ErrorTextBlock.Visibility = Visibility.Collapsed;

            string email = EmailTextBox.Text.Trim();
            string password = PasswordBox.Password;

            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                ErrorTextBlock.Text = "Wprowadź email i hasło.";
                ErrorTextBlock.Visibility = Visibility.Visible;
                return;
            }

            var request = new LoginRequest
            {
                Email = email,
                Password = password
            };

            try
            {
                var response = await _httpClient.PostAsJsonAsync("/api/auth/login", request);
                if (response.IsSuccessStatusCode)
                {
                    var token = await response.Content.ReadFromJsonAsync<TokenResponse>();

                    if (token != null)
                    {
                        App.Current.Properties["jwtToken"] = token.Token;
                        App.Current.Properties["userEmail"] = email;

                        this.DialogResult = true; // WAŻNE! TUTAJ DialogResult=true
                    }
                    else
                    {
                        ErrorTextBlock.Text = "Błąd autoryzacji. Spróbuj ponownie.";
                        ErrorTextBlock.Visibility = Visibility.Visible;
                    }
                }
                else
                {
                    ErrorTextBlock.Text = "Nieprawidłowy email lub hasło.";
                    ErrorTextBlock.Visibility = Visibility.Visible;
                }
            }
            catch
            {
                ErrorTextBlock.Text = "Nie można połączyć się z serwerem.";
                ErrorTextBlock.Visibility = Visibility.Visible;
            }
        }

        private void RegisterLink_Click(object sender, RoutedEventArgs e)
        {
            var registerWindow = new RegisterWindow();
            var result = registerWindow.ShowDialog();

            if (result == true)
            {
                ErrorTextBlock.Text = "Rejestracja zakończona, możesz się zalogować.";
                ErrorTextBlock.Visibility = Visibility.Visible;
            }
        }
    }
}
