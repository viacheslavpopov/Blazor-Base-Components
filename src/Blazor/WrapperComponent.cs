// Copyright (c) 2020 Allan Mobley. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Components;

namespace Mobsites.Blazor
{
    /// <summary>
    /// Abstract base for a helper component that is used to wrap or contain content.
    /// </summary>
    public abstract class WrapperComponent : BaseComponent<IParentComponentBase>, IParentComponentBase
    {
        /****************************************************
        *
        *  PUBLIC INTERFACE
        *
        ****************************************************/
        
        /// <summary>
        /// Content to wrap.
        /// </summary>
        [Parameter] public RenderFragment ChildContent { get; set; }

        private ContrastModes contrastMode;

        /// <summary>
        /// Switch for dark and light modes. 
        /// Setting this has no effect on this component.
        /// Use BackgroundMode to set a dark or light mode.
        /// </summary>
        [Parameter] public override ContrastModes ContrastMode
        {
            get => this.Parent is null
                ? this.BackgroundMode switch
                {
                    BackgroundModes.Dark => ContrastModes.Dark,
                    BackgroundModes.Light => ContrastModes.Light,
                    _ => ContrastModes.Normal
                }
                : this.Parent.ContrastMode;
            set => contrastMode = value;
        }
    }
}