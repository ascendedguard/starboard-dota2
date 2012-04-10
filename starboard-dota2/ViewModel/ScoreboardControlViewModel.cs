// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ScoreboardControlViewModel.cs" company="Starboard">
//   Copyright © 2011 All Rights Reserved
// </copyright>
// <summary>
//   ViewModel controlling all the information necessary for databinding the scoreboards.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Starboard.ViewModel
{
    using System.Collections.ObjectModel;

    using Starboard.Model;
    using Starboard.MVVM;

    /// <summary>
    /// ViewModel controlling all the information necessary for databinding the scoreboards.
    /// </summary>
    public class ScoreboardControlViewModel : ObservableObject
    {
        #region Constants and Fields

        /// <summary>
        /// Holds our first player, which is initialized on creation.
        /// </summary>
        private readonly Player player1 = new Player();

        /// <summary>
        /// Holds our second player, which is initialized on creation.
        /// </summary>
        private readonly Player player2 = new Player();

        private int winsRequired = 2;

        /// <summary>
        /// Backing property for the isSubbarShowing property.
        /// </summary>
        private bool isSubbarShowing;

        #endregion

        #region Public Properties

        public int WinsRequired
        {
            get
            {
                return this.winsRequired;
            }

            set
            {
                this.winsRequired = value;
                this.RaisePropertyChanged("WinsRequired");
            }
        }

        /// <summary>
        ///   Gets or sets a value indicating whether the subbar is showing.
        /// </summary>
        public bool IsSubbarShowing
        {
            get
            {
                return this.isSubbarShowing;
            }

            set
            {
                this.isSubbarShowing = value;
                this.RaisePropertyChanged("IsSubbarShowing");
            }
        }

        /// <summary>
        ///   Gets the first player's information.
        /// </summary>
        public Player Player1
        {
            get
            {
                return this.player1;
            }
        }

        /// <summary>
        ///   Gets the second player's information.
        /// </summary>
        public Player Player2
        {
            get
            {
                return this.player2;
            }
        }

        /// <summary>
        /// Swap the players between the left and right sides of the scoreboard.
        /// </summary>
        public void SwapPlayers()
        {
            var p1 = (Player)this.Player2.Clone();
            var p2 = (Player)this.Player1.Clone();

            this.Player1.Name = p1.Name;
            this.Player1.Score = p1.Score;

            this.Player2.Name = p2.Name;
            this.Player2.Score = p2.Score;
        }

        #endregion
    }
}