// Copyright (c) 2020 Allan Mobley. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

namespace Mobsites.Blazor
{
    public enum ContrastModes
    {
        Normal,
        Dark,
        Light
    }
    
    public enum BackgroundModes
    {
        None,
        Dark,
        Light,
        Image,
        Gradient,
        Color
    }

    public enum BackgroundColorDirections
    {
        BottomToTop = 0,
        LeftToRight = 90,
        TopToBottom = 180,
        RightToLeft = 270
    }
}