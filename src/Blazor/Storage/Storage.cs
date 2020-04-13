// Copyright (c) 2020 Allan Mobley. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using Microsoft.JSInterop;

namespace Mobsites.Blazor
{
    /// <summary>
    /// Members for accessing a browser's storage.
    /// </summary>
    public class Storage
    {
        public Storage(IJSRuntime jsRuntime)
        {
            Local = new LocalStorage(jsRuntime);
            Session = new SessionStorage(jsRuntime);
        }

        internal const string KeyPrefix = "Mobsites.Blazor.";

        /// <summary>
        /// Member for accessing a browser's local storage.
        /// </summary>
        public LocalStorage Local { get; set; }

        /// <summary>
        /// Member for accessing a browser's session storage.
        /// </summary>
        public SessionStorage Session { get; set; }
    }
}