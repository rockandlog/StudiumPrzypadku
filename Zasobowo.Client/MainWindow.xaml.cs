using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Zasobowo.Client.Models;
using Zasobowo.Client.Services;
using System.Threading.Tasks;

namespace Zasobowo.Client
{
    public partial class MainWindow : Window
    {
        private readonly DeviceServiceClient _deviceService;
        private readonly UserServiceClient _userService;
        private List<Device> _devices = new();
        private List<User> _users = new();
        private Device? _selectedDevice;

        public MainWindow()
        {
            InitializeComponent();
            _deviceService = new DeviceServiceClient();
            _userService = new UserServiceClient();
            LoadData();
        }

        private async void LoadData()
        {
            try
            {
                _devices = await _deviceService.GetDevicesAsync();
                _users = await _userService.GetUsersAsync();

                DeviceGrid.ItemsSource = _devices;
                UserComboBox.ItemsSource = _users;

                int count = _devices.Count;
                string suffix = count switch
                {
                    1 => "urządzenie",
                    2 or 3 or 4 => "urządzenia",
                    _ => "urządzeń"
                };

                StatusTextBlock.Text = $"✅ Załadowano {count} {suffix}.";
            }
            catch (System.Exception ex)
            {
                StatusTextBlock.Text = $"❌ Błąd ładowania: {ex.Message}";
            }
        }

        private async void AddDevice_Click(object sender, RoutedEventArgs e)
        {
            var name = NameTextBox.Text.Trim();
            var type = (TypeComboBox.SelectedItem as ComboBoxItem)?.Content?.ToString();
            var status = (StatusComboBox.SelectedItem as ComboBoxItem)?.Content?.ToString();
            var user = UserComboBox.SelectedItem as User;

            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(type) || string.IsNullOrWhiteSpace(status))
            {
                StatusTextBlock.Text = "❌ Wypełnij wszystkie pola: Nazwa, Typ, Status.";
                return;
            }

            if (status == "Przydzielony" && user == null)
            {
                StatusTextBlock.Text = "❌ Przydzielone urządzenie wymaga przypisanego użytkownika.";
                return;
            }

            var device = new Device
            {
                Name = name,
                Type = type,
                Status = status,
                AssignedUserId = user?.Id
            };

            try
            {
                await _deviceService.AddDeviceAsync(device);
                StatusTextBlock.Text = "✅ Dodano urządzenie.";
                ClearInputs();
                LoadData();
            }
            catch (System.Exception ex)
            {
                StatusTextBlock.Text = $"❌ Błąd dodawania: {ex.Message}";
            }
        }

        private async void UpdateDevice_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedDevice == null)
            {
                StatusTextBlock.Text = "❌ Najpierw wybierz urządzenie z listy.";
                return;
            }

            var name = NameTextBox.Text.Trim();
            var type = (TypeComboBox.SelectedItem as ComboBoxItem)?.Content?.ToString();
            var status = (StatusComboBox.SelectedItem as ComboBoxItem)?.Content?.ToString();
            var user = UserComboBox.SelectedItem as User;

            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(type) || string.IsNullOrWhiteSpace(status))
            {
                StatusTextBlock.Text = "❌ Wypełnij wszystkie pola: Nazwa, Typ, Status.";
                return;
            }

            if (status == "Przydzielony" && user == null)
            {
                StatusTextBlock.Text = "❌ Przydzielone urządzenie wymaga przypisanego użytkownika.";
                return;
            }

            _selectedDevice.Name = name;
            _selectedDevice.Type = type;
            _selectedDevice.Status = status;
            _selectedDevice.AssignedUserId = user?.Id;

            try
            {
                await _deviceService.UpdateDeviceAsync(_selectedDevice);
                StatusTextBlock.Text = "✅ Zaktualizowano urządzenie.";
                ClearInputs();
                LoadData();
            }
            catch (System.Exception ex)
            {
                StatusTextBlock.Text = $"❌ Błąd aktualizacji: {ex.Message}";
            }
        }

        private async void DeleteDevice_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedDevice == null)
            {
                StatusTextBlock.Text = "❌ Najpierw wybierz urządzenie do usunięcia.";
                return;
            }

            try
            {
                await _deviceService.DeleteDeviceAsync(_selectedDevice.Id);
                StatusTextBlock.Text = "✅ Usunięto urządzenie.";
                ClearInputs();
                LoadData();
            }
            catch (System.Exception ex)
            {
                StatusTextBlock.Text = $"❌ Błąd usuwania: {ex.Message}";
            }
        }

        private void DeviceGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedDevice = DeviceGrid.SelectedItem as Device;

            if (_selectedDevice != null)
            {
                NameTextBox.Text = _selectedDevice.Name;
                SetComboBoxByValue(TypeComboBox, _selectedDevice.Type);
                SetComboBoxByValue(StatusComboBox, _selectedDevice.Status);

                if (_selectedDevice.AssignedUserId.HasValue)
                    UserComboBox.SelectedValue = _selectedDevice.AssignedUserId.Value;
                else
                    UserComboBox.SelectedIndex = -1;
            }
        }

        private void SetComboBoxByValue(ComboBox comboBox, string value)
        {
            foreach (ComboBoxItem item in comboBox.Items)
            {
                if ((item.Content?.ToString() ?? "") == value)
                {
                    comboBox.SelectedItem = item;
                    break;
                }
            }
        }

        private void ClearInputs()
        {
            NameTextBox.Text = "";
            TypeComboBox.SelectedIndex = -1;
            StatusComboBox.SelectedIndex = -1;
            UserComboBox.SelectedIndex = -1;
            _selectedDevice = null;
            DeviceGrid.SelectedIndex = -1;
        }

        private void ClearForm_Click(object sender, RoutedEventArgs e)
        {
            ClearInputs();
            StatusTextBlock.Text = "🧼 Formularz został wyczyszczony.";
        }
    }
}
