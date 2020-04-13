// Copyright (c) 2020 Allan Mobley. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;

namespace Mobsites.Blazor
{
    /// <summary>
    /// Abstract base for a component that must be a descendant of a component that implements the <see cref="IParentComponentBase" /> interface.
    /// </summary>
    public abstract class GrandChildComponent<T> : ChildBaseComponent<T>
        where T : IParentComponentBase
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