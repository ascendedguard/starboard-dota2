// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WinCounter.xaml.cs" company="Starboard">
//   Copyright © 2011 All Rights Reserved
// </copyright>
// <summary>
//   Interaction logic for WinCounter.xaml
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Starboard.View
{
    using System.Windows;

    /// <summary>
    /// Interaction logic for WinCounter.xaml
    /// </summary>
    public partial class WinCounter
    {
        /// <summary> Dependency property for the WinRequired property. </summary>
        public static readonly DependencyProperty WinsRequiredProperty =
            DependencyProperty.Register("WinsRequired", typeof(int), typeof(WinCounter), new UIPropertyMetadata(2, ScoreChanged));

        // Using a DependencyProperty as the backing store for Wins.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty WinsProperty =
            DependencyProperty.Register("Wins", typeof(int), typeof(WinCounter), new UIPropertyMetadata(0, WinsChanged));

        /// <summary> Initializes a new instance of the <see cref="WinCounter"/> class. </summary>
        public WinCounter()
        {
            InitializeComponent();

            this.UpdateScore();
        }

        /// <summary>
        /// Gets or sets the amount of wins required to win the match.
        /// </summary>
        public int WinsRequired
        {
            get { return (int)GetValue(WinsRequiredProperty); }
            set { SetValue(WinsRequiredProperty, value); }
        }

        /// <summary>
        /// Gets or sets the number of wins for the team.
        /// </summary>
        public int Wins
        {
            get { return (int)GetValue(WinsProperty); }
            set { SetValue(WinsProperty, value); }
        }

        private static void WinsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var counter = (WinCounter)d;
            counter.UpdateScore();
        }

        public bool IsReversed
        {
            get { return (bool)GetValue(IsReversedProperty); }
            set { SetValue(IsReversedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsReversed.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsReversedProperty =
            DependencyProperty.Register("IsReversed", typeof(bool), typeof(WinCounter), new UIPropertyMetadata(false, ScoreChanged));

        private static void ScoreChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var counter = (WinCounter)d;
            counter.UpdateScore();
        }

        private void UpdateScore()
        {
            var value = this.Wins;

            if (this.IsReversed)
            {
                tick1.IsActive = value >= 3;
                tick2.IsActive = value >= 2;
                tick3.IsActive = value >= 1;   

                this.tick1.Visibility = this.WinsRequired >= 3 ? Visibility.Visible : Visibility.Collapsed;
                this.tick2.Visibility = this.WinsRequired >= 2 ? Visibility.Visible : Visibility.Collapsed;
                this.tick3.Visibility = Visibility.Visible;
            }
            else
            {
                tick3.IsActive = value >= 3;
                tick2.IsActive = value >= 2;
                tick1.IsActive = value >= 1;

                this.tick3.Visibility = this.WinsRequired >= 3 ? Visibility.Visible : Visibility.Collapsed;
                this.tick2.Visibility = this.WinsRequired >= 2 ? Visibility.Visible : Visibility.Collapsed;
                this.tick1.Visibility = Visibility.Visible;
            }
        }
    }
}
