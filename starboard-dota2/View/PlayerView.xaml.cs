// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PlayerView.xaml.cs" company="Starboard">
//   Copyright © 2011 All Rights Reserved
// </copyright>
// <summary>
//   Interaction logic for PlayerView.xaml
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Starboard.View
{
    using System;
    using System.Windows;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;

    using Starboard.Model;

    /// <summary>
    /// Interaction logic for PlayerView.xaml
    /// </summary>
    public partial class PlayerView
    {
        #region Constructors and Destructors

        /// <summary> Initializes a new instance of the <see cref = "PlayerView" /> class. </summary>
        public PlayerView()
        {
            this.InitializeComponent();
        }

        #endregion

        #region Methods

        /// <summary> Increment's the player's score by 1. </summary>
        /// <param name="sender"> The sender.  </param>
        /// <param name="e"> The event arguments.  </param>
        private void IncrementPlayerScore(object sender, RoutedEventArgs e)
        {
            var player = this.DataContext as Player;

            if (player != null)
            {
                player.Score++;
            }
        }

        #endregion


        private void ImageDrop(object sender, DragEventArgs e)
        {
            var data = e.Data;
            var isData = data.GetDataPresent(DataFormats.FileDrop);
            var strArr = (string[])data.GetData(DataFormats.FileDrop);

            var file = strArr[0]; // Only pay attention to the first file.

            var player = this.DataContext as Player;

            var img = new BitmapImage(new Uri(file));
            player.TeamLogo = img;
        }

        private void ClearTeamLogo(object sender, RoutedEventArgs e)
        {
            var player = this.DataContext as Player;
            player.TeamLogo = null;
        }
    }
}