// Copyright (c) 2020 Allan Mobley. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mobsites.Blazor
{
    /// <summary>
    /// Abstract base to all other abstract components, providing common members and functionality needed by all.
    /// </summary>
    public abstract partial class BaseComponent<T> : ComponentBase, IDisposable
        where T : IParentComponentBase
    {
        /****************************************************
        *
        *  PUBLIC INTERFACE
        *
        ****************************************************/

        /// <summary>
        /// Reference to parent component (if any).
        /// </summary>
        [CascadingParameter] public virtual T Parent { get; set; }

        /// <summary>
        /// Css classes for directly affecting this component go here.
        /// </summary>
        [Parameter] public virtual string Class { get; set; }

        private string style;

        /// <summary>
        /// Styles for directly affecting this component go here.
        /// </summary>
        [Parameter] public virtual string Style
        { 
            get 
            {
                string color = string.IsNullOrWhiteSpace(this.Color) ? null : $"color: {this.Color};";
                string background = string.IsNullOrWhiteSpace(this.BackgroundColor) ? null : $"background-color: {this.BackgroundColor};";

                return background + color + style;
            }
            set => style = value; 
        }

        /// <summary>
        /// All attributes outside of the class and style attributes go here.
        /// </summary>
        [Parameter(CaptureUnmatchedValues = true)] public virtual Dictionary<string, object> ExtraAttributes { get; set; }

        /// <summary>
        /// The style of background to apply.
        /// </summary>
        [Parameter] public virtual BackgroundModes BackgroundMode { get; set; }

        /// <summary>
        /// Call back event for notifying another component that this property changed. 
        /// </summary>
        [Parameter] public virtual EventCallback<BackgroundModes> BackgroundModeChanged { get; set; }

        private ContrastModes contrastMode;

        /// <summary>
        /// Switch for dark and light modes.
        /// </summary>
        [Parameter] public virtual ContrastModes ContrastMode
        {
            get => (this.Parent?.ContrastMode ?? ContrastModes.Normal) == ContrastModes.Normal ? contrastMode : this.Parent.ContrastMode;
            set => contrastMode = value;
        } 

        /// <summary>
        /// Call back event for notifying another component that this property changed. 
        /// </summary>
        [Parameter] public virtual EventCallback<ContrastModes> ContrastModeChanged { get; set; }

        /// <summary>
        /// Whether to inherit a parent's colors (dark, light, or normal modes).
        /// </summary>
        [Parameter] public virtual bool InheritParentColors { get; set; }

        /// <summary>
        /// Call back event for notifying another component that this property changed. 
        /// </summary>
        [Parameter] public virtual EventCallback<bool> InheritParentColorsChanged { get; set; }

        private string color;

        /// <summary>
        /// The foreground color for this component. Accepts any valid css color usage.
        /// </summary>
        [Parameter] public virtual string Color 
        { 
            get => this.Parent is null
                ? this.ContrastMode switch
                {
                    ContrastModes.Dark => this.DarkModeColor,
                    ContrastModes.Light => this.LightModeColor,
                    _ => color
                }
                : this.Parent.ContrastMode switch
                {
                    ContrastModes.Dark => InheritParentColors
                        ? this.Parent.Color
                        : this.DarkModeColor,
                    ContrastModes.Light => InheritParentColors
                        ? this.Parent.Color
                        : this.LightModeColor,
                    _ => InheritParentColors
                        ? this.Parent.Color
                        : color
                };
            set => color = string.IsNullOrWhiteSpace(value) ? null : value;
        }

        /// <summary>
        /// Call back event for notifying another component that this property changed. 
        /// </summary>
        [Parameter] public virtual EventCallback<string> ColorChanged { get; set; }

        /// <summary>
        /// Whether to inherit a parent's background colors (dark, light, or normal modes).
        /// </summary>
        [Parameter] public virtual bool InheritParentBackgroundColors { get; set; }

        /// <summary>
        /// Call back event for notifying another component that this property changed. 
        /// </summary>
        [Parameter] public virtual EventCallback<bool> InheritParentBackgroundColorsChanged { get; set; }

        private string backgroundColor;

        /// <summary>
        /// The background color for this component. Accepts any valid css color usage.
        /// </summary>
        [Parameter] public virtual string BackgroundColor 
        { 
            get => this.Parent is null
                ? this.ContrastMode switch
                {
                    ContrastModes.Dark => this.DarkModeBackgroundColor,
                    ContrastModes.Light => this.LightModeBackgroundColor,
                    _ => backgroundColor
                }
                : this.Parent.ContrastMode switch
                {
                    ContrastModes.Dark => InheritParentBackgroundColors
                        ? this.Parent.BackgroundColor
                        : this.DarkModeBackgroundColor,
                    ContrastModes.Light => InheritParentBackgroundColors
                        ? this.Parent.BackgroundColor
                        : this.LightModeBackgroundColor,
                    _ => InheritParentBackgroundColors
                        ? this.Parent.BackgroundColor
                        : backgroundColor
                };
            set => backgroundColor = string.IsNullOrWhiteSpace(value) ? null : value;
        }
        
        /// <summary>
        /// Call back event for notifying another component that this property changed. 
        /// </summary>
        [Parameter] public virtual EventCallback<string> BackgroundColorChanged { get; set; }
        
        /// <summary>
        /// The background color for this component's dark mode. Default is #121212.
        /// Accepts any valid css color usage.
        /// </summary>
        [Parameter] public virtual string DarkModeBackgroundColor { get; set; } = "#121212";

        /// <summary>
        /// The foreground color for this component's dark mode. Default is #f2f2f2.
        /// Accepts any valid css color usage.
        /// </summary>
        [Parameter] public virtual string DarkModeColor { get; set; } = "#f2f2f2";

        /// <summary>
        /// The background color for this component's light mode. Default is #f2f2f2.
        /// Accepts any valid css color usage.
        /// </summary>
        [Parameter] public virtual string LightModeBackgroundColor { get; set; } = "#f2f2f2";

        /// <summary>
        /// The foreground color for this component's light mode. Default is #121212.
        /// Accepts any valid css color usage.
        /// </summary>
        [Parameter] public virtual string LightModeColor { get; set; } = "#121212";



        /****************************************************
        *
        *  NON-PUBLIC INTERFACE
        *
        ****************************************************/

        /// <summary>
        /// Whether the component has been completely initialized, including any JavaScript representation.
        /// </summary>
        protected bool initialized { get; set; }

        /// <summary>
        /// Injected JavaScript interop.
        /// </summary>
        [Inject] protected IJSRuntime jsRuntime { get; set; }

        protected void SetOptions(BaseComponentOptions options)
        {
            options.BackgroundMode = this.BackgroundMode;
            options.ContrastMode = this.ContrastMode;
            options.InheritParentColors = this.InheritParentColors;
            options.InheritParentBackgroundColors = this.InheritParentBackgroundColors;
            options.Color = this.Color;
            options.BackgroundColor = this.BackgroundColor;
        }

        protected async Task<bool> CheckState(BaseComponentOptions options)
        {
            bool stateChanged = false;

            if (this.BackgroundMode != (options.BackgroundMode ?? BackgroundModes.None))
            {
                this.BackgroundMode = options.BackgroundMode ?? BackgroundModes.None;
                await this.BackgroundModeChanged.InvokeAsync(this.BackgroundMode);
                stateChanged = true;
            }
            if (this.ContrastMode != (options.ContrastMode ?? ContrastModes.Normal))
            {
                this.ContrastMode = options.ContrastMode ?? ContrastModes.Normal;
                await this.ContrastModeChanged.InvokeAsync(this.ContrastMode);
                stateChanged = true;
            }
            if (this.InheritParentColors != options.InheritParentColors)
            {
                this.InheritParentColors = options.InheritParentColors;
                await this.InheritParentColorsChanged.InvokeAsync(this.InheritParentColors);
                stateChanged = true;
            }
            if (this.InheritParentBackgroundColors != options.InheritParentBackgroundColors)
            {
                this.InheritParentBackgroundColors = options.InheritParentBackgroundColors;
                await this.InheritParentBackgroundColorsChanged.InvokeAsync(this.InheritParentBackgroundColors);
                stateChanged = true;
            }
            if (this.Color != options.Color)
            {
                this.Color = options.Color;
                await this.ColorChanged.InvokeAsync(this.Color);
                stateChanged = true;
            }
            if (this.BackgroundColor != options.BackgroundColor)
            {
                this.BackgroundColor = options.BackgroundColor;
                await this.BackgroundColorChanged.InvokeAsync(this.BackgroundColor);
                stateChanged = true;
            }

            return stateChanged;
        }   

        public virtual void Dispose()
        {
            initialized = false;
        }
    }
}