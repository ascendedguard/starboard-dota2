﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ScoreboardControlPanelViewModel.cs" company="Starboard">
//   Copyright © 2011 All Rights Reserved
// </copyright>
// <summary>
//   View model for the main scoreboard control view, which allows the user to
//   change the player information, show the scoreboard, and all other critical updates.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Starboard.ViewModel
{
    using System.Windows.Input;

    using Starboard.Model;
    using Starboard.MVVM;

    /// <summary>
    /// The scoreboard control panel view model.
    /// </summary>
    public class ScoreboardControlPanelViewModel : ObservableObject
    {
        #region Constants and Fields

        /// <summary> Reference to the active scoreboard view model. </summary>
        private readonly ScoreboardControlViewModel scoreboard;

        /// <summary> The backing field for the swap players command. </summary>
        private ICommand swapPlayersCommand;

        /// <summary> The backing field for the toggle scoreboard visible command. </summary>
        private ICommand toggleScoreboardVisibleCommand;

        /// <summary> The backing field for the toggle subbar command. </summary>
        private ICommand toggleSubbarCommand;
        
        private ICommand bestOf1Command;        
        private ICommand bestOf3Command;
        private ICommand bestOf5Command;

        #endregion

        #region Constructors and Destructors

        /// <summary> Initializes a new instance of the <see cref="ScoreboardControlPanelViewModel"/> class. </summary>
        /// <param name="scoreboard"> The scoreboard view model. </param>
        public ScoreboardControlPanelViewModel(ScoreboardControlViewModel scoreboard)
        {
            this.scoreboard = scoreboard;
        }

        #endregion

        #region Public Properties

        

        /// <summary> Gets the active scoreboard view model. </summary>
        public ScoreboardControlViewModel Scoreboard
        {
            get
            {
                return this.scoreboard;
            }
        }

        /// <summary> Gets a command to swap the players on the scoreboard. </summary>
        public ICommand SwapPlayersCommand
        {
            get
            {
                return this.swapPlayersCommand ?? (this.swapPlayersCommand = new RelayCommand(this.SwapPlayers));
            }
        }

        public ICommand BestOf1Command
        {
            get
            {
                return this.bestOf1Command ?? (this.bestOf1Command = new RelayCommand(this.SetBestOf1));
            }
        }

        public ICommand BestOf3Command
        {
            get
            {
                return this.bestOf3Command ?? (this.bestOf3Command = new RelayCommand(this.SetBestOf3));
            }
        }

        public ICommand BestOf5Command
        {
            get
            {
                return this.bestOf5Command ?? (this.bestOf5Command = new RelayCommand(this.SetBestOf5));
            }
        }

        private void SetBestOf1()
        {
            this.Scoreboard.WinsRequired = 1;
        }

        private void SetBestOf3()
        {
            this.Scoreboard.WinsRequired = 2;
        }

        private void SetBestOf5()
        {
            this.Scoreboard.WinsRequired = 3;
        }

        /// <summary> Gets a command to toggle whether the scoreboard is visible. </summary>
        public ICommand ToggleScoreboardVisibleCommand
        {
            get
            {
                return this.toggleScoreboardVisibleCommand
                       ?? (this.toggleScoreboardVisibleCommand = new RelayCommand(this.ToggleScoreboardVisible));
            }
        }

        /// <summary> Gets a command to toggle whether the subbar is visible. </summary>
        public ICommand ToggleSubbarCommand
        {
            get
            {
                return this.toggleSubbarCommand ?? (this.toggleSubbarCommand = new RelayCommand(this.ToggleSubbar));
            }
        }

        #endregion

        #region Public Methods

        /// <summary> Swaps the position of the two players, flipping left and right sides. </summary>
        public void SwapPlayers()
        {
            var player2 = (Player)this.Scoreboard.Player1.Clone();
            var player1 = (Player)this.Scoreboard.Player2.Clone();

            this.Scoreboard.Player1.Name = player1.Name;
            this.Scoreboard.Player1.Score = player1.Score;
            this.Scoreboard.Player1.TeamLogo = player1.TeamLogo;

            this.Scoreboard.Player2.Name = player2.Name;
            this.Scoreboard.Player2.Score = player2.Score;
            this.Scoreboard.Player2.TeamLogo = player2.TeamLogo;
        }

        /// <summary> Toggles the visibility of the scoreboard. </summary>
        public void ToggleScoreboardVisible()
        {
            var display = MainWindowViewModel.DisplayWindow;

            if (display.IsVisible)
            {
                display.Hide();
            }
            else
            {
                display.Show();
            }
        }

        /// <summary> Toggles whether the subbar is visible. </summary>
        public void ToggleSubbar()
        {
            this.Scoreboard.IsSubbarShowing = !this.Scoreboard.IsSubbarShowing;
        }

        #endregion
    }
}