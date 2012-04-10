// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AbilityTick.xaml.cs" company="Starboard">
//   Copyright © 2011 All Rights Reserved
// </copyright>
// <summary>
//   Interaction logic for AbilityTick.xaml
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Starboard.View
{
    using System.Windows;

    /// <summary>
    /// Interaction logic for AbilityTick.xaml
    /// </summary>
    public partial class AbilityTick
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AbilityTick"/> class.
        /// </summary>
        public AbilityTick()
        {
            InitializeComponent();
        }

        // Whether the tick mark is active (lit up)
        private bool isActive = false;

        /// <summary>
        /// Gets or sets a value indicating whether the tick mark is lit up.
        /// </summary>
        public bool IsActive
        {
            get
            {
                return this.isActive;    
            }

            set
            {
                this.isActive = value;

                this.imgEnabled.Visibility = this.isActive ? Visibility.Visible : Visibility.Collapsed;
                this.imgDisabled.Visibility = this.isActive ? Visibility.Collapsed : Visibility.Visible;
            }
        }
    }
}
