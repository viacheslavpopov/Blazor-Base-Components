// Copyright (c) 2020 Allan Mobley. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using Microsoft.AspNetCore.Components;

namespace Mobsites.Blazor
{
    /// <summary>
    /// Abstract base for a UI component that exists solely for the benefit of its specified parent,
    /// which must inherit the <see cref="MainComponent" /> base.
    /// </summary>
    public abstract class Subcomponent<T> : BaseComponent<T>, IParentComponentBase
        where T : MainComponent
    {
        /****************************************************
        *
        *  NON-PUBLIC INTERFACE
        *
        ****************************************************/

        /// <summary>
        /// The style of background to apply.
        /// </summary>
        protected new BackgroundModes BackgroundMode { get; set; }

        /// <summary>
        /// Call back event for notifying another component that this property changed. 
        /// </summary>
        protected new EventCallback<BackgroundModes> BackgroundModeChanged { get; set; }

        protected override void OnParametersSet()
        {
            if (Parent is null)
            {
                throw new ArgumentNullException(
                    nameof(Parent), 
                    $"This component must be nested in a parent component of type {typeof(T).Name}!");
            }
        }
    }
}