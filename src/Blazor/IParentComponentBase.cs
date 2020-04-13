// Copyright (c) 2020 Allan Mobley. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

namespace Mobsites.Blazor
{
    // <summary>
    /// Interface describing members a parent component should have.
    /// </summary>
    public interface IParentComponentBase
    {
        ContrastModes ContrastMode { get; set; }
        string Color { get; set; }
        string DarkModeColor { get; set; }
        string LightModeColor { get; set; }
        string BackgroundColor { get; set; }
        string DarkModeBackgroundColor { get; set; }
        string LightModeBackgroundColor { get; set; }
    }
}