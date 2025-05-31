using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Zasobowo.Client.Models;
using Zasobowo.Client.Services;

namespace Zasobowo.Client
{
    public partial class MainWindow : Window
    {
        private readonly DeviceServiceClient _api = new();
        private List<Device> _devices = new();
        private Device? _selectedDevice;

        public MainWindow()
        {
            InitializeComponent();
            LoadDevices();
        }

        private async void LoadDevices()
        {
            _devices = await _api.GetDevicesAsync();
            DeviceGrid.ItemsSource = _devices;
        }

        private void DeviceGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DeviceGrid.SelectedItem is Device device)
            {
                _selectedDevice = device;
                IdBox.Text = device.Id.ToString();
                NameBox.Text = device.Name;
                StatusBox.Text = device.Status;
                AssignedToBox.Text = device.AssignedTo;
                TypeBox.Text = device.Type;
            }
        }

        private async void AddDevice_Click(object sender, RoutedEventArgs e)
        {
            var device = new Device
            {
                Name = NameBox.Text,
                Status = StatusBox.Text,
                AssignedTo = AssignedToBox.Text,
                Type = TypeBox.Text
            };

            await _api.AddDeviceAsync(device);
            LoadDevices();
            ClearForm();
        }

        private async void UpdateDevice_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedDevice == null) return;

            _selectedDevice.Name = NameBox.Text;
            _selectedDevice.Status = StatusBox.Text;
            _selectedDevice.AssignedTo = AssignedToBox.Text;
            _selectedDevice.Type = TypeBox.Text;

            await _api.AddDeviceAsync(_selectedDevice);
            LoadDevices();
            ClearForm();
        }

        private async void DeleteDevice_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedDevice == null) return;

            await _api.DeleteDeviceAsync(_selectedDevice.Id);
            LoadDevices();
            ClearForm();
        }

        private void ClearForm_Click(object sender, RoutedEventArgs e)
        {
            ClearForm();
        }

        private void ClearForm()
        {
            IdBox.Text = "";
            NameBox.Text = "";
            StatusBox.Text = "";
            AssignedToBox.Text = "";
            TypeBox.Text = "";
            _selectedDevice = null;
            DeviceGrid.SelectedItem = null;
        }
    }
}
