// Copyright (c) 2020 Allan Mobley. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;

namespace Mobsites.Blazor
{
    /// <summary>
    /// Abstract base for a component that must be a descendant of a component that inherits <see cref="MainComponent" />.
    /// </summary>
    public abstract class ChildComponent<T> : ChildBaseComponent<T>, IParentComponentBase
        where T : MainComponent
    {
        protected override void OnParametersSet()
        {
            if (Parent is null)
            {
                throw new ArgumentNullException(nameof(Parent), $"This component must have a parent component of type {typeof(T).Name}!");
            }
        }
    }
}