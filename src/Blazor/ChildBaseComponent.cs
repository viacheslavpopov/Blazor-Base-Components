// Copyright (c) 2020 Allan Mobley. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Components;

namespace Mobsites.Blazor
{
    /// <summary>
    /// Abstract base for a child component that may or may not be a descendant of a component that inherits <see cref="MainComponent" />.
    /// </summary>
    public abstract class ChildBaseComponent<T> : BaseComponent
        where T : IParentComponentBase
    {
        /// <summary>
        /// Reference to parent component.
        /// </summary>
        [CascadingParameter] public T Parent { get; set; }

        private string style;

        /// <summary>
        /// Styles for directly affecting this component go here.
        /// </summary>
        [Parameter] public override string Style
        { 
            get 
            {
                string color = string.IsNullOrWhiteSpace(this.Color) ? null : $"color: {this.Color};";
                string background = string.IsNullOrWhiteSpace(this.BackgroundColor) ? null : $"background-color: {this.BackgroundColor};";

                return background + color + style;
            }
            set => style = value; 
        }

        private ContrastModes contrastMode;

        /// <summary>
        /// Switch for dark and light modes.
        /// </summary>
        [Parameter] public override ContrastModes ContrastMode
        {
            get => (this.Parent?.ContrastMode ?? ContrastModes.Normal) == ContrastModes.Normal ? contrastMode : this.Parent.ContrastMode;
            set => contrastMode = value;
        } 

        private string color;

        /// <summary>
        /// The foreground color for this component. Accepts any valid css color usage.
        /// </summary>
        [Parameter] public override string Color 
        { 
            get => this.Parent is null
                ? this.ContrastMode switch
                {
                    ContrastModes.Dark => this.DarkModeColor ?? color,
                    ContrastModes.Light => this.LightModeColor ?? color,
                    _ => color
                }
                : this.Parent.ContrastMode switch
                {
                    ContrastModes.Dark => this.DarkModeColor ?? this.Parent.DarkModeColor ?? color ?? this.Parent.Color,
                    ContrastModes.Light => this.LightModeColor ?? this.Parent.LightModeColor ?? color ?? this.Parent.Color,
                    _ => color ?? this.Parent.Color
                };
            set => color = string.IsNullOrWhiteSpace(value) ? null : value; 
        }

        private string backgroundColor;

        /// <summary>
        /// The background color for this component. Accepts any valid css color usage.
        /// </summary>
        [Parameter] public override string BackgroundColor 
        { 
            get => this.Parent is null
                ? this.ContrastMode switch
                {
                    ContrastModes.Dark => this.DarkModeBackgroundColor ?? backgroundColor,
                    ContrastModes.Light => this.LightModeBackgroundColor ?? backgroundColor,
                    _ => backgroundColor
                }
                : this.Parent.ContrastMode switch
                {
                    ContrastModes.Dark => this.DarkModeBackgroundColor ?? this.Parent.DarkModeBackgroundColor ?? backgroundColor ?? this.Parent.BackgroundColor,
                    ContrastModes.Light => this.LightModeBackgroundColor ?? this.Parent.LightModeBackgroundColor ?? backgroundColor ?? this.Parent.BackgroundColor,
                    _ => backgroundColor ?? this.Parent.BackgroundColor
                };
            set => backgroundColor = string.IsNullOrWhiteSpace(value) ? null : value; 
        }
    }
}