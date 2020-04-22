// Copyright (c) 2020 Allan Mobley. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Components;

namespace Mobsites.Blazor
{
    /// <summary>
    /// Abstract base for a useful UI component that may need to keep state.
    /// </summary>
    public abstract class StatefulComponent : MainComponent
    {
        /****************************************************
        *
        *  PUBLIC INTERFACE
        *
        ****************************************************/

        /// <summary>
        /// Whether this component should store state in browser storage.
        /// </summary>
        [Parameter] public bool KeepState { get; set; }

        /// <summary>
        /// Whether this component should use session storage to keep state. Default is local storage.
        /// </summary>
        [Parameter] public bool UseSessionStorageForState { get; set; }

        /// <summary>
        /// Unique id for storage.
        /// Note: This is only necessary for components that are used more than once in an app
        /// and each requires its own unique state.
        /// </summary>
        [Parameter] public string Id { get; set; }



        /****************************************************
        *
        *  NON-PUBLIC INTERFACE
        *
        ****************************************************/

        /// <summary>
        /// Access to storage in browser.
        /// </summary>
        protected Storage Storage { get; set; }

        protected override void OnInitialized()
        {
            Storage = new Storage(jsRuntime);
        }

        protected string GetKey<T>()
            where T : StatefulComponent => string.IsNullOrWhiteSpace(Id) ? typeof(T).Name : $"{typeof(T).Name}.{Id}";
    }
}