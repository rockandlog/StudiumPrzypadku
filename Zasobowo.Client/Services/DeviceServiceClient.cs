<<<<<<< HEAD
﻿using System;
=======
>>>>>>> develop
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Zasobowo.Client.Models;

namespace Zasobowo.Client.Services
{
    public class DeviceServiceClient
    {
<<<<<<< HEAD
        private readonly HttpClient _http;

        public DeviceServiceClient()
        {
            _http = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7031")
            };

            if (App.Current.Properties.Contains("jwtToken"))
            {
                var token = App.Current.Properties["jwtToken"] as string;
                _http.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }
=======
        private readonly HttpClient _httpClient;
        private readonly string _baseApiUrl = "https://localhost:7031/api/device"; // Upewnij się, że port pasuje do API

        public DeviceServiceClient()
        {
            _httpClient = new HttpClient();
>>>>>>> develop
        }

        public async Task<List<Device>> GetDevicesAsync()
        {
<<<<<<< HEAD
            return await _http.GetFromJsonAsync<List<Device>>("/api/Device");
        }

        public async Task AddDeviceAsync(Device device)
        {
            var response = await _http.PostAsJsonAsync("/api/Device", device);
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateDeviceAsync(Device device)
        {
            var response = await _http.PutAsJsonAsync($"/api/Device/{device.Id}", device);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteDeviceAsync(int id)
        {
            var response = await _http.DeleteAsync($"/api/Device/{id}");
            response.EnsureSuccessStatusCode();
=======
            var response = await _httpClient.GetFromJsonAsync<List<Device>>(_baseApiUrl);
            return response ?? new List<Device>();
        }

        public async Task<bool> AddDeviceAsync(Device device)
        {
            var response = await _httpClient.PostAsJsonAsync(_baseApiUrl, device);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteDeviceAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{_baseApiUrl}/{id}");
            return response.IsSuccessStatusCode;
>>>>>>> develop
        }
    }
}
