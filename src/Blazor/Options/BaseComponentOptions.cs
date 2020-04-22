// Copyright (c) 2020 Allan Mobley. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

namespace Mobsites.Blazor
{
    /// <summary>
    /// Abstract base representing options that can be saved in browser storage.
    /// </summary>
    public abstract class BaseComponentOptions
    {
        /************************************************************************
        *
        *   Non-null enum and int members with a zero value do not need to be
        *   serialized as they would default to zero on C# object initialization.
        *   
        *   (For nullable types...well null is null.)
        *
        *   Setting their options equivalent to null will keep them from
        *   being serialized.
        *
        *   This saves space in browser storage.
        *
        *   Caveat: If the options are passed into a javascript function,
        *   then, obviously, any such members depended on there will have to 
        *   be accounted for there as not defined or null and, thus,
        *   equivalent to zero.
        *
        ***********************************************************************/

        private BackgroundModes? backgroundMode;
        public BackgroundModes? BackgroundMode 
        { 
            get => backgroundMode; 
            set => backgroundMode = this.NullOnZero<BackgroundModes?>(value); 
        }

        private ContrastModes? contrastMode;
        public ContrastModes? ContrastMode 
        { 
            get => contrastMode; 
            set => contrastMode = this.NullOnZero<ContrastModes?>(value); 
        }

        public bool InheritParentColors { get; set; }

        public bool InheritParentBackgroundColors { get; set; }

        public string Color { get; set; }

        public string BackgroundColor { get; set; }

        protected T NullOnZero<T>(object value) => (int)value == 0 ? default : (T)value;
    }
}