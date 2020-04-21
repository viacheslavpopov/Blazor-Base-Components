// Copyright (c) 2020 Allan Mobley. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Mobsites.Blazor
{
    /// <summary>
    /// Abstract base for a useful UI component.
    /// </summary>
    public abstract class MainComponent : BaseComponent<IParentComponentBase>, IParentComponentBase
    {
        /****************************************************
        *
        *  PUBLIC INTERFACE
        *
        ****************************************************/

        private string style;

        /// <summary>
        /// Styles for directly affecting this component go here.
        /// </summary>
        [Parameter] public override string Style
        { 
            get 
            {
                string color = string.IsNullOrWhiteSpace(this.Color) 
                    ? null 
                    : $"color: {this.Color};";
                string backgroundColor = string.IsNullOrWhiteSpace(this.BackgroundColor) 
                    ? null 
                    : $"background: {this.BackgroundColor};";
                string image = string.IsNullOrWhiteSpace(this.BackgroundImage) 
                    ? backgroundColor 
                    : $"background: url('{this.BackgroundImage}') no-repeat center/cover;";
                string gradient = string.IsNullOrWhiteSpace(this.BackgroundColorStart) || string.IsNullOrWhiteSpace(this.BackgroundColorEnd)
                    ? backgroundColor 
                    : $"background: linear-gradient({(int)(this.BackgroundColorDirection)}deg, {this.BackgroundColorStart} 0%, {this.BackgroundColorEnd} 70%);";
                string background = BackgroundMode switch
                {
                    BackgroundModes.None => null,
                    BackgroundModes.Image => image,
                    BackgroundModes.Gradient => gradient,
                    _ => backgroundColor
                };

                return background + color + style;
            }
            set => style = value; 
        }

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

        /// <summary>
        /// The background color for this component. Accepts any valid css color usage.
        /// </summary>
        [Parameter] public override string BackgroundColor
        {
            get => BackgroundMode == BackgroundModes.None ? null : base.BackgroundColor;
            set => base.BackgroundColor = value;
        }

        private string backgroundImage;
        
        /// <summary>
        /// Background image source. Set BackgroundMode to Image for usage.
        /// </summary>
        [Parameter] public virtual string BackgroundImage 
        { 
            get => backgroundImage; 
            set 
            { 
                if (!string.IsNullOrEmpty(value))
                {
                    backgroundImage = value;
                } 
            } 
        }

        /// <summary>
        /// The direction of background color flow. Set BackgroundMode to Gradient for usage.
        /// </summary>
        [Parameter] public virtual BackgroundColorDirections BackgroundColorDirection { get; set; }

        /// <summary>
        /// Call back event for notifying another component that this property changed. 
        /// </summary>
        [Parameter] public virtual EventCallback<BackgroundColorDirections> BackgroundColorDirectionChanged { get; set; }

        /// <summary>
        /// The gradient start color for this component. Accepts any valid css color usage.
        /// Set BackgroundMode to Gradient for usage.
        /// </summary>
        [Parameter] public virtual string BackgroundColorStart { get; set; }

        /// <summary>
        /// Call back event for notifying another component that this property changed. 
        /// </summary>
        [Parameter] public virtual EventCallback<string> BackgroundColorStartChanged { get; set; }

        /// <summary>
        /// The gradient end color for this component. Accepts any valid css color usage.
        /// Set BackgroundMode to Gradient for usage.
        /// </summary>
        [Parameter] public virtual string BackgroundColorEnd { get; set; }

        /// <summary>
        /// Call back event for notifying another component that this property changed. 
        /// </summary>
        [Parameter] public virtual EventCallback<string> BackgroundColorEndChanged { get; set; }



        /****************************************************
        *
        *  NON-PUBLIC INTERFACE
        *
        ****************************************************/

        protected void SetOptions(MainComponentOptions options)
        {
            options.BackgroundColorDirection = this.BackgroundColorDirection;
            options.BackgroundColorStart = this.BackgroundColorStart;
            options.BackgroundColorEnd = this.BackgroundColorEnd;

            base.SetOptions(options);
        }

        protected async Task<bool> CheckState(MainComponentOptions options)
        {
            bool stateChanged = false;

            if (this.BackgroundColorDirection != (options.BackgroundColorDirection ?? BackgroundColorDirections.BottomToTop))
            {
                this.BackgroundColorDirection = options.BackgroundColorDirection ?? BackgroundColorDirections.BottomToTop;
                await this.BackgroundColorDirectionChanged.InvokeAsync(this.BackgroundColorDirection);
                stateChanged = true;
            }
            if (this.BackgroundColorStart != options.BackgroundColorStart)
            {
                this.BackgroundColorStart = options.BackgroundColorStart;
                await this.BackgroundColorStartChanged.InvokeAsync(this.BackgroundColorStart);
                stateChanged = true;
            }
            if (this.BackgroundColorEnd != options.BackgroundColorEnd)
            {
                this.BackgroundColorEnd = options.BackgroundColorEnd;
                await this.BackgroundColorEndChanged.InvokeAsync(this.BackgroundColorEnd);
                stateChanged = true;
            }

            return await base.CheckState(options) || stateChanged;
        }   
    }
}