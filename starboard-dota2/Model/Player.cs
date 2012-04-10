// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Player.cs" company="Starboard">
//   Copyright © 2011 All Rights Reserved
// </copyright>
// <summary>
//   Model for a Player, containing the player's name, color, race and score.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Starboard.Model
{
    using System;
    using System.Windows.Input;
    using System.Windows.Media;

    using Starboard.MVVM;

    /// <summary> Model for a Player, containing the player's name, color, race and score.</summary>
    public class Player : ObservableObject, ICloneable
    {
        /// <summary> Backing for the ResetCommand command. </summary>
        private ICommand resetCommand;

        /// <summary> The player's name, defaulting to "Player". </summary>
        private string name = string.Empty;

        /// <summary> The current score, starting at 0. </summary>
        private int score;

        private ImageSource teamLogo;

        /// <summary> Gets a command which resets the player. </summary>
        public ICommand ResetCommand
        {
            get
            {
                return this.resetCommand ?? (this.resetCommand = new RelayCommand(this.Reset));
            }
        }

        public ImageSource TeamLogo
        {
            get
            {
                return this.teamLogo;
            }

            set
            {
                this.teamLogo = value;
                this.RaisePropertyChanged("TeamLogo");
            }
        }

        /// <summary> Gets or sets the name of the player. </summary>
        public string Name
        {
            get 
            { 
                return this.name; 
            }

            set
            {
                this.name = value;
                RaisePropertyChanged("Name");
            }
        }

        /// <summary> Gets or sets the player's current score. </summary>
        public int Score
        {
            get
            {
                return this.score;
            }

            set
            {
                this.score = value < 0 ? 0 : value;
                RaisePropertyChanged("Score");
            }
        }

        /// <summary> Resets the player back to default status. </summary>
        public void Reset()
        {
            this.Name = string.Empty;
            this.Score = 0;
            this.TeamLogo = null;
        }

        public object Clone()
        {
            var player = new Player { Name = this.Name, Score = this.Score, TeamLogo = this.TeamLogo };

            return player;
        }
    }
}
