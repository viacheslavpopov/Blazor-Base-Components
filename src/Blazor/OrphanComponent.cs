// Copyright (c) 2020 Allan Mobley. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Mobsites.Blazor
{
    /// <summary>
    /// Abstract stateful base for a component that may or may not be a descendant of a component that inherits <see cref="MainComponent" />.
    /// </summary>
    public abstract class OrphanComponent<T> : ChildBaseComponent<T>, IParentComponentBase
        where T : MainComponent
    {
        /// <summary>
        /// Whether this component should store state in browser storage.
        /// </summary>
        [Parameter] public bool KeepState { get; set; }

        /// <summary>
        /// Whether this component should use session storage to keep state. Default is local storage.
        /// </summary>
        [Parameter] public bool UseSessionStorageForState { get; set; }

        /// <summary>
        /// Injected JavaScript interop.
        /// </summary>
        [Inject] protected IJSRuntime jsRuntime { get; set; }

        /// <summary>
        /// Access to storage in browser.
        /// </summary>
        protected Storage Storage { get; set; }

        protected override void OnInitialized()
        {
            Storage = new Storage(jsRuntime);
        }

        protected void SetOptions(OptionsBase options)
        {
            options.Color = this.Color;
            options.ContrastMode = this.ContrastMode;
            options.BackgroundColor = this.BackgroundColor;
        }

        protected async Task CheckState(OptionsBase options)
        {
            if (this.Color != options.Color)
            {
                await this.ColorChanged.InvokeAsync(options.Color);
            }
            if (this.ContrastMode != options.ContrastMode)
            {
                await this.ContrastModeChanged.InvokeAsync(options.ContrastMode);
            }
            if (this.BackgroundColor != options.BackgroundColor)
            {
                await this.BackgroundColorChanged.InvokeAsync(options.BackgroundColor);
            }
        } 
    }
}