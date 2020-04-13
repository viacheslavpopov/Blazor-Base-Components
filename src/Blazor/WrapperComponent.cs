// Copyright (c) 2020 Allan Mobley. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Components;

namespace Mobsites.Blazor
{
    /// <summary>
    /// Abstract component base for wrapping content. 
    /// </summary>
    public abstract class WrapperComponent : BaseComponent
    {
        /// <summary>
        /// Content to wrap.
        /// </summary>
        [Parameter] public RenderFragment ChildContent { get; set; }
    }
}