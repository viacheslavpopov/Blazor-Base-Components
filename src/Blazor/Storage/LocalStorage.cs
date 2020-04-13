// Copyright (c) 2020 Allan Mobley. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using Microsoft.JSInterop;
using System.Text.Json;
using System.Threading.Tasks;

namespace Mobsites.Blazor
{
    /// <summary>
    /// Members for accessing a browser's local storage.
    /// </summary>
    public class LocalStorage
    {
        private readonly IJSRuntime jsRuntime;
        private readonly JsonSerializerOptions options;

        public LocalStorage(IJSRuntime jsRuntime)
        {
            this.jsRuntime = jsRuntime;
            options = new JsonSerializerOptions
            {
                IgnoreNullValues = true,
                AllowTrailingCommas = true,
                PropertyNameCaseInsensitive = true
            };
        }

        /// <summary>
        /// Save <typeparamref name="T" /> value to the browser's local storage using the specified key.
        /// </summary>
        public ValueTask SetAsync<T>(string key, T value)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new System.ArgumentException("message", nameof(key));
            }

            key = Storage.KeyPrefix + key + "." + typeof(T).Name;

            return jsRuntime.InvokeVoidAsync("localStorage.setItem", key, JsonSerializer.Serialize(value, options));
        }

        /// <summary>
        /// Retrieve <typeparamref name="T" /> value from the browser's local storage using the specified key.
        /// </summary>
        public async ValueTask<T> GetAsync<T>(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new System.ArgumentException("message", nameof(key));
            }

            key = Storage.KeyPrefix + key + "." + typeof(T).Name;

            var json = await jsRuntime.InvokeAsync<string>("localStorage.getItem", key);

            return string.IsNullOrEmpty(json)
                ? default
                : JsonSerializer.Deserialize<T>(json, options);
        }

        /// <summary>
        /// Remove <typeparamref name="T" /> value from the browser's local storage using the specified key.
        /// </summary>
        public ValueTask RemoveAsync<T>(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new System.ArgumentException("message", nameof(key));
            }

            key = Storage.KeyPrefix + key + "." + typeof(T).Name;

            return jsRuntime.InvokeVoidAsync("localStorage.removeItem", key);
        }
    }
}