// Copyright (c) 2020 Allan Mobley. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Mobsites.Blazor
{
    /// <summary>
    /// Abstract stateful base for a main component in Blazor.
    /// </summary>
    public abstract class MainComponent : BaseComponent, IParentComponentBase
    {
        private string style;

        /// <summary>
        /// Styles for directly affecting this component go here.
        /// </summary>
        [Parameter] public override string Style
        { 
            get 
            {
                string color = string.IsNullOrWhiteSpace(this.Color) ? null : $"color: {this.Color};";
                string background = BackgroundMode switch
                {
                    BackgroundModes.None => null,
                    BackgroundModes.Image => $"background: url('{this.BackgroundImage}') no-repeat center;",
                    BackgroundModes.Gradient => $"background: linear-gradient({(int)(this.BackgroundColorDirection)}deg, {this.BackgroundColorStart} 0%, {this.BackgroundColorEnd} 70%);",
                    _ => $"background: {this.BackgroundColor};"
                };

                return background + color + style;
            }
            set => style = value; 
        }

        private ContrastModes contrastMode;

        /// <summary>
        /// Switch for dark and light modes. 
        /// Setting this has no effect on this component.
        /// Use BackgroundMode to set a dark or light mode.
        /// </summary>
        [Parameter] public override ContrastModes ContrastMode
        {
            get => this.BackgroundMode switch
            {
                BackgroundModes.Dark => ContrastModes.Dark,
                BackgroundModes.Light => ContrastModes.Light,
                _ => ContrastModes.Normal
            };
            set => contrastMode = value;
        } 

        private string color;

        /// <summary>
        /// The foreground color for this component. Accepts any valid css color usage.
        /// </summary>
        [Parameter] public override string Color 
        { 
            get => BackgroundMode switch
            {
                BackgroundModes.Dark => DarkModeColor,
                BackgroundModes.Light => LightModeColor,
                _ => color
            };
            set => color = value; 
        }

        private string backgroundColor;

        /// <summary>
        /// The background color for this component. Accepts any valid css color usage.
        /// </summary>
        [Parameter] public override string BackgroundColor
        { 
            get => BackgroundMode switch
            {
                BackgroundModes.Dark => DarkModeBackgroundColor,
                BackgroundModes.Light => LightModeBackgroundColor,
                _ => backgroundColor
            };
            set => backgroundColor = value; 
        }

        /// <summary>
        /// The style of background to apply.
        /// </summary>
        [Parameter] public BackgroundModes BackgroundMode { get; set; }

        /// <summary>
        /// Call back event for notifying another component that this property changed. 
        /// </summary>
        [Parameter] public EventCallback<BackgroundModes> BackgroundModeChanged { get; set; }

        private string backgroundImage;
        
        /// <summary>
        /// Background image source. Set BackgroundMode to Image for usage.
        /// </summary>
        [Parameter] public string BackgroundImage 
        { 
            get => backgroundImage; 
            set 
            { 
                if (!string.IsNullOrEmpty(value))
                {
                    backgroundImage = value;
                } 
            } 
        }

        /// <summary>
        /// The direction of background color flow. Set BackgroundMode to Gradient for usage.
        /// </summary>
        [Parameter] public BackgroundColorDirections BackgroundColorDirection { get; set; }

        /// <summary>
        /// Call back event for notifying another component that this property changed. 
        /// </summary>
        [Parameter] public EventCallback<BackgroundColorDirections> BackgroundColorDirectionChanged { get; set; }

        /// <summary>
        /// The gradient start color for this component. Accepts any valid css color usage.
        /// Set BackgroundMode to Gradient for usage.
        /// </summary>
        [Parameter] public string BackgroundColorStart { get; set; }

        /// <summary>
        /// Call back event for notifying another component that this property changed. 
        /// </summary>
        [Parameter] public EventCallback<string> BackgroundColorStartChanged { get; set; }

        /// <summary>
        /// The gradient end color for this component. Accepts any valid css color usage.
        /// Set BackgroundMode to Gradient for usage.
        /// </summary>
        [Parameter] public string BackgroundColorEnd { get; set; }

        /// <summary>
        /// Call back event for notifying another component that this property changed. 
        /// </summary>
        [Parameter] public EventCallback<string> BackgroundColorEndChanged { get; set; }

        /// <summary>
        /// The background color for this component's dark mode. Default is rgba(0, 0, 0, 0.92).
        /// Accepts any valid css color usage.
        /// </summary>
        [Parameter] public override string DarkModeBackgroundColor { get; set; } = "rgba(0, 0, 0, 0.92)";

        /// <summary>
        /// The foreground color for this component's dark mode. Default is rgba(255, 255, 255, 1).
        /// Accepts any valid css color usage.
        /// </summary>
        [Parameter] public override string DarkModeColor { get; set; } = "rgba(255, 255, 255, 1)";

        /// <summary>
        /// The background color for this component's light mode. Default is rgba(255, 255, 255, 1).
        /// Accepts any valid css color usage.
        /// </summary>
        [Parameter] public override string LightModeBackgroundColor { get; set; } = "rgba(255, 255, 255, 1)";

        /// <summary>
        /// The foreground color for this component's light mode. Default is rgba(0, 0, 0, 0.92).
        /// Accepts any valid css color usage.
        /// </summary>
        [Parameter] public override string LightModeColor { get; set; } = "rgba(0, 0, 0, 0.92)";

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
            options.BackgroundMode = this.BackgroundMode;
            options.BackgroundColor = this.BackgroundColor;
            options.BackgroundColorDirection = this.BackgroundColorDirection;
            options.BackgroundColorStart = this.BackgroundColorStart;
            options.BackgroundColorEnd = this.BackgroundColorEnd;
        }

        protected async Task CheckState(OptionsBase options)
        {
            if (this.Color != options.Color)
            {
                await this.ColorChanged.InvokeAsync(options.Color);
            }
            if (this.BackgroundMode != options.BackgroundMode)
            {
                await this.BackgroundModeChanged.InvokeAsync(options.BackgroundMode);
            }
            if (this.BackgroundColor != options.BackgroundColor)
            {
                await this.BackgroundColorChanged.InvokeAsync(options.BackgroundColor);
            }
            if (this.BackgroundColorDirection != options.BackgroundColorDirection)
            {
                await this.BackgroundColorDirectionChanged.InvokeAsync(options.BackgroundColorDirection);
            }
            if (this.BackgroundColorStart != options.BackgroundColorStart)
            {
                await this.BackgroundColorStartChanged.InvokeAsync(options.BackgroundColorStart);
            }
            if (this.BackgroundColorEnd != options.BackgroundColorEnd)
            {
                await this.BackgroundColorEndChanged.InvokeAsync(options.BackgroundColorEnd);
            }
        }   
    }
}