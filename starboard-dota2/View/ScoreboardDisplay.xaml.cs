﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ScoreboardDisplay.xaml.cs" company="Starboard">
//   Copyright © 2011 All Rights Reserved
// </copyright>
// <summary>
//   Interaction logic for ScoreboardDisplay.xaml
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Starboard.View
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Input;
    using System.Windows.Media.Animation;

    using Starboard.Model;
    using Starboard.ViewModel;

    /// <summary>
    /// Interaction logic for ScoreboardDisplay.xaml
    /// </summary>
    public partial class ScoreboardDisplay
    {
        // Using a DependencyProperty as the backing store for Scoreboard.  This enables animation, styling, binding, etc...
        #region Constants and Fields

        /// <summary>
        /// The scoreboard property.
        /// </summary>
        public static readonly DependencyProperty ScoreboardProperty = DependencyProperty.Register(
            "Scoreboard", typeof(ScoreboardControlViewModel), typeof(ScoreboardDisplay), new UIPropertyMetadata(null));

        /// <summary>
        ///   The opacity used by the scoreboard when visible. The maximum value used during transitions.
        /// </summary>
        private double maxOpacity = 1;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///   Initializes a new instance of the <see cref = "ScoreboardDisplay" /> class.
        /// </summary>
        public ScoreboardDisplay()
        {
            this.InitializeComponent();

            this.InitializePositionOnLoad = true;

            this.Loaded += this.WindowLoaded;
            this.IsWindowMovable = false;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///   Gets or sets a value indicating whether the window should reset positions when first loaded.
        /// </summary>
        public bool InitializePositionOnLoad { get; set; }

        /// <summary>
        ///   Gets or sets a value indicating whether the window can be dragged.
        /// </summary>
        public bool IsWindowMovable { get; set; }

        /// <summary>
        ///   Gets or sets the maximum opacity used by the scoreboard.
        /// </summary>
        public double MaxOpacity
        {
            get
            {
                return this.maxOpacity;
            }

            set
            {
                this.maxOpacity = value;
                if (this.IsVisible)
                {
                    // Applying the opacity without an animation results in no change. Is there a better way?
                    var animation = new DoubleAnimation(value, new Duration(new TimeSpan(0, 0, 0, 0)));
                    this.BeginAnimation(OpacityProperty, animation);
                }
            }
        }

        /// <summary>
        /// Gets or sets Scoreboard.
        /// </summary>
        public ScoreboardControlViewModel Scoreboard
        {
            get
            {
                return (ScoreboardControlViewModel)this.GetValue(ScoreboardProperty);
            }

            set
            {
                this.SetValue(ScoreboardProperty, value);
            }
        }

        /// <summary>
        ///   Gets or sets the width of the viewbox used to render the scoreboard.
        /// </summary>
        public double ViewboxWidth
        {
            get
            {
                return this.viewBox.Width;
            }

            set
            {
                this.viewBox.Width = value;
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Overrides the original Hide function to support fading in cases where transparency is used.
        /// </summary>
        public new void Hide()
        {
            if (this.AllowsTransparency)
            {
                var animation = new DoubleAnimation(0, new Duration(new TimeSpan(0, 0, 0, 0, 500)));
                Action hideAction = base.Hide;
                animation.Completed += (sender, e) => hideAction();
                this.BeginAnimation(OpacityProperty, animation);
            }
            else
            {
                base.Hide();
            }
        }

        /// <summary>
        /// Resets the position of the window to the default location, centered on the primary monitor with a 10px offset from top.
        /// </summary>
        public void ResetPosition()
        {
            if (this.IsMeasureValid == false)
            {
                this.UpdateLayout();
                this.Measure(new Size(SystemParameters.PrimaryScreenWidth, SystemParameters.PrimaryScreenHeight));
            }

            var leftAdjust = this.Width / 2.0;

            var left = (SystemParameters.PrimaryScreenWidth / 2.0) - leftAdjust;

            this.Left = left;
            this.Top = 0.0;
        }

        /// <summary>
        /// Sets the viewmodel for the window to another instance of ScoreboardControlViewModel
        /// </summary>
        /// <param name="vm">
        /// The viewModel to apply.  
        /// </param>
        public void SetViewModel(ScoreboardControlViewModel vm)
        {
            this.Scoreboard = vm;
        }

        /// <summary>
        /// Overrides the original Show function to support a fade-in effect in cases where transparency is used.
        /// </summary>
        public new void Show()
        {
            if (this.AllowsTransparency)
            {
                this.Opacity = 0;
            }

            base.Show();

            if (this.AllowsTransparency)
            {
                var animation = new DoubleAnimation(this.MaxOpacity, new Duration(new TimeSpan(0, 0, 0, 0, 500)));
                this.BeginAnimation(OpacityProperty, animation);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Overrides the OnKeyDown event to handled hotkeys for this window.
        /// </summary>
        /// <param name="e">
        /// The event arguments.  
        /// </param>
        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.Key == Key.F1)
            {
                // Change Player 1's Name
                this.CreatePlayerChangeField("Player1.Name");
            }
            else if (e.Key == Key.F2)
            {
                // Change Player 2's Name
                this.CreatePlayerChangeField("Player2.Name");
            }
            else if (e.Key == Key.F3)
            {
                this.Scoreboard.SwapPlayers();
            }

            if (e.KeyboardDevice.Modifiers == ModifierKeys.Control)
            {
                var result = ApplyHotkey(e.Key, this.Scoreboard.Player1);
                e.Handled = result;
            }

            if (e.KeyboardDevice.Modifiers == ModifierKeys.Alt && e.SystemKey != Key.LeftAlt)
            {
                var result = ApplyHotkey(e.SystemKey, this.Scoreboard.Player2);
                e.Handled = result;
            }
        }

        /// <summary>
        /// Allows the window to be dragged if IsWindowMovable has been set.
        /// </summary>
        /// <param name="e">
        /// The event arguments.  
        /// </param>
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);

            if (this.IsWindowMovable)
            {
                this.DragMove();
            }
        }

        /// <summary>
        /// Applies the hotkey for the attached key to the attached player.
        /// </summary>
        /// <param name="key">
        /// The key which was pressed in the hotkey sequence.  
        /// </param>
        /// <param name="player">
        /// The player to apply the change to.  
        /// </param>
        /// <returns>
        /// Whether the key pressed was a valid hotkey, and was applied to the player.  
        /// </returns>
        private static bool ApplyHotkey(Key key, Player player)
        {
            var handled = true;

            switch (key)
            {
                case Key.OemPlus:
                case Key.Add:
                    player.Score++;
                    break;
                case Key.OemMinus:
                case Key.Subtract:
                    player.Score--;
                    break;
                default:
                    handled = false;
                    break;
            }

            return handled;
        }

        /// <summary>
        /// Creates a text field for typing the player name directly into the scoreboard. Clears the player name upon the hotkey press.
        /// </summary>
        /// <param name="binding">
        /// The binding to apply to the TextBox.  
        /// </param>
        private void CreatePlayerChangeField(string binding)
        {
            var field = new TextBox();
            field.SetBinding(
                TextBox.TextProperty, 
                new Binding(binding)
                    {
                       Source = this.Scoreboard, UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged 
                    });
            field.Width = 50;
            field.Height = 20;

            field.LostFocus += (sender, e) => this.rootGrid.Children.Remove(field);
            field.KeyDown += (sender, e) =>
                {
                    if (e.Key == Key.Enter || e.Key == Key.Return || e.Key == Key.Escape)
                    {
                        this.rootGrid.Children.Remove(field);
                    }
                };

            this.rootGrid.Children.Insert(0, field);

            field.Focus();
            field.Text = string.Empty;
        }

        /// <summary>
        /// Resets the position of the window after it has completed loading.
        /// </summary>
        /// <param name="sender">
        /// The sender. 
        /// </param>
        /// <param name="e">
        /// The event parameters. 
        /// </param>
        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            if (this.InitializePositionOnLoad)
            {
                this.ResetPosition();
            }
        }

        #endregion
    }
}