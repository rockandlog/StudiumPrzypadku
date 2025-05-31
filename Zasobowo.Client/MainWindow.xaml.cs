using Microsoft.AspNetCore.SignalR.Client;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using Zasobowo.Client.Models;
using Zasobowo.Client.Services;

namespace Zasobowo.Client
{
    public partial class MainWindow : Window
    {
        private readonly DeviceServiceClient _deviceService = new();
        private ObservableCollection<DeviceDto> _devices = new();
        private List<UserDto> _users = new();
        private HubConnection _hubConnection;
        private DeviceDto? selectedDevice;

        public MainWindow()
        {
            InitializeComponent();
            Loaded += async (_, _) =>
            {
                await InitializeHub();
                LoadDevices();
                LoadUsers();
            };
        }

        private async Task InitializeHub()
        {
            _hubConnection = new HubConnectionBuilder()
                .WithUrl("https://localhost:7031/synchub")
                .WithAutomaticReconnect()
                .Build();

            _hubConnection.On("DeviceUpdated", () =>
            {
                Dispatcher.Invoke(() => LoadDevices());
            });

            await _hubConnection.StartAsync();
        }

        private async void LoadDevices()
        {
            var devices = await _deviceService.GetAllDevicesAsync();
            _devices = new ObservableCollection<DeviceDto>(devices);
            DeviceGrid.ItemsSource = _devices;
        }

        private async void LoadUsers()
        {
            _users = await _deviceService.GetAllUsersAsync();
            UserComboBox.ItemsSource = _users;
        }

        private void DeviceGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedDevice = DeviceGrid.SelectedItem as DeviceDto;

            if (selectedDevice != null)
            {
                NameTextBox.Text = selectedDevice.Name;
                TypeComboBox.Text = selectedDevice.Type;
                StatusComboBox.Text = selectedDevice.Status;

                if (selectedDevice.AssignedUserId != null)
                    UserComboBox.SelectedValue = selectedDevice.AssignedUserId;
                else
                    UserComboBox.SelectedIndex = -1;
            }
        }

        private async void AddDevice_Click(object sender, RoutedEventArgs e)
        {
            var device = new DeviceDto
            {
                Name = NameTextBox.Text.Trim(),
                Type = (TypeComboBox.SelectedItem as ComboBoxItem)?.Content?.ToString() ?? "",
                Status = (StatusComboBox.SelectedItem as ComboBoxItem)?.Content?.ToString() ?? "",
                AssignedUserId = (int?)UserComboBox.SelectedValue
            };

            var result = await _deviceService.CreateDeviceAsync(device);
            StatusTextBlock.Text = result;
        }

        private async void UpdateDevice_Click(object sender, RoutedEventArgs e)
        {
            if (selectedDevice == null)
            {
                StatusTextBlock.Text = "Najpierw wybierz urządzenie do aktualizacji.";
                return;
            }

            var updatedDevice = new DeviceDto
            {
                Id = selectedDevice.Id,
                Name = NameTextBox.Text.Trim(),
                Type = (TypeComboBox.SelectedItem as ComboBoxItem)?.Content?.ToString() ?? "",
                Status = (StatusComboBox.SelectedItem as ComboBoxItem)?.Content?.ToString() ?? "",
                AssignedUserId = (int?)UserComboBox.SelectedValue
            };

            var result = await _deviceService.UpdateDeviceAsync(updatedDevice.Id, updatedDevice);
            StatusTextBlock.Text = result;
        }

        private async void DeleteDevice_Click(object sender, RoutedEventArgs e)
        {
            if (selectedDevice == null)
            {
                StatusTextBlock.Text = "Najpierw wybierz urządzenie do usunięcia.";
                return;
            }

            var result = await _deviceService.DeleteDeviceAsync(selectedDevice.Id);
            StatusTextBlock.Text = result;
            ClearForm();
        }

        private void ClearForm_Click(object sender, RoutedEventArgs e)
        {
            ClearForm();
        }

        private void ClearForm()
        {
            selectedDevice = null;
            NameTextBox.Text = "";
            TypeComboBox.SelectedIndex = -1;
            StatusComboBox.SelectedIndex = -1;
            UserComboBox.SelectedIndex = -1;
        }
    }
}
