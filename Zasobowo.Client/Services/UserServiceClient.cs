<<<<<<< HEAD
﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Zasobowo.Client.Models;

namespace Zasobowo.Client.Services
{
    public class UserServiceClient
    {
        private readonly HttpClient _http;

        public UserServiceClient()
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
        }

        public async Task<List<User>> GetUsersAsync()
        {
            return await _http.GetFromJsonAsync<List<User>>("/api/Users");
        }
=======
﻿using System.Net.Http;
using System.Net.Http.Json;

public class UserServiceClient
{
    private readonly HttpClient _httpClient;

    public UserServiceClient()
    {
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri("https://localhost:7031/api/");
    }

    public async Task<List<User>> GetUsersAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<User>>("user");
>>>>>>> develop
    }
}
