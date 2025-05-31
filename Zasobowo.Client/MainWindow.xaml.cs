using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Windows;
using System.Windows.Controls;
using Zasobowo.Client.Models;

namespace Zasobowo.Client
{
    public partial class MainWindow : Window
    {
        private readonly HttpClient _httpClient;
        private List<DeviceDto> _devices = new();

        public MainWindow()
        {
            InitializeComponent();
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7031")
            };
            LoadDevices();
        }

        private async void LoadDevices()
        {
            try
            {
                _devices = await _httpClient.GetFromJsonAsync<List<DeviceDto>>("/api/Device");
                DeviceGrid.ItemsSource = _devices;
                StatusTextBlock.Text = $"✅ Załadowano {_devices.Count} urządzeń.";
            }
            catch (Exception ex)
            {
                StatusTextBlock.Text = $"❌ Błąd ładowania danych: {ex.Message}";
            }
        }

        private void DeviceGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Tu możesz rozbudować pod edycję itp.
        }

        private void AddDevice_Click(object sender, RoutedEventArgs e) { /* ... */ }
        private void UpdateDevice_Click(object sender, RoutedEventArgs e) { /* ... */ }
        private void DeleteDevice_Click(object sender, RoutedEventArgs e) { /* ... */ }
        private void ClearForm_Click(object sender, RoutedEventArgs e) { /* ... */ }
    }
}
