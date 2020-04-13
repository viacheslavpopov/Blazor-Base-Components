// Copyright (c) 2020 Allan Mobley. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace Mobsites.Blazor
{
    /// <summary>
    /// Abstract base for all of our Blazor components.
    /// </summary>
    public abstract partial class BaseComponent : ComponentBase
    {
        /// <summary>
        /// Css classes for directly affecting this component go here.
        /// </summary>
        [Parameter] public virtual string Class { get; set; }

        /// <summary>
        /// Styles for directly affecting this component go here.
        /// </summary>
        [Parameter] public virtual string Style { get; set; }

        /// <summary>
        /// All attributes outside of the class and style attributes go here.
        /// </summary>
        [Parameter(CaptureUnmatchedValues = true)] public virtual Dictionary<string, object> ExtraAttributes { get; set; }

        /// <summary>
        /// Switch for dark and light modes.
        /// </summary>
        [Parameter] public virtual ContrastModes ContrastMode { get; set; }

        /// <summary>
        /// Call back event for notifying another component that this property changed. 
        /// </summary>
        [Parameter] public virtual EventCallback<ContrastModes> ContrastModeChanged { get; set; }

        /// <summary>
        /// The foreground color for this component. Accepts any valid css color usage.
        /// </summary>
        [Parameter] public virtual string Color { get; set; }

        /// <summary>
        /// Call back event for notifying another component that this property changed. 
        /// </summary>
        [Parameter] public virtual EventCallback<string> ColorChanged { get; set; }

        /// <summary>
        /// The background color for this component. Accepts any valid css color usage.
        /// </summary>
        [Parameter] public virtual string BackgroundColor { get; set; }

        /// <summary>
        /// Call back event for notifying another component that this property changed. 
        /// </summary>
        [Parameter] public virtual EventCallback<string> BackgroundColorChanged { get; set; }

        /// <summary>
        /// The background color for this component's dark mode. Accepts any valid css color usage.
        /// </summary>
        [Parameter] public virtual string DarkModeBackgroundColor { get; set; }

        /// <summary>
        /// The foreground color for this component's dark mode. Accepts any valid css color usage.
        /// </summary>
        [Parameter] public virtual string DarkModeColor { get; set; }

        /// <summary>
        /// The background color for this component's light mode. Accepts any valid css color usage.
        /// </summary>
        [Parameter] public virtual string LightModeBackgroundColor { get; set; }

        /// <summary>
        /// The foreground color for this component's light mode. Accepts any valid css color usage.
        /// </summary>
        [Parameter] public virtual string LightModeColor { get; set; }
    }
}