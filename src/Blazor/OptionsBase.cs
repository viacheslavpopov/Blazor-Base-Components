// Copyright (c) 2020 Allan Mobley. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

namespace Mobsites.Blazor
{
    /// <summary>
    /// Abstract base representing base component options that can be saved in browser storage.
    /// </summary>
    public abstract class OptionsBase
    {
        public ContrastModes ContrastMode { get; set; }
        public BackgroundModes BackgroundMode { get; set; }
        public bool UseDarkMode { get; set; }
        public bool UseLightMode { get; set; }
        public bool UseColor { get; set; }
        public string Color { get; set; }
        public bool UseBackgroundColor { get; set; }
        public string BackgroundColor { get; set; }
        public BackgroundColorDirections BackgroundColorDirection { get; set; }
        public string BackgroundColorStart { get; set; }
        public string BackgroundColorEnd { get; set; }
    }
}