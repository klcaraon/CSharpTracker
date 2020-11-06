using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Insta.Graph.API.Logic;
using Insta.Graph.Entity;

namespace CSharpTracker
{
    /// <summary>
    /// Interaction logic for InstagramWindow.xaml
    /// </summary>
    public partial class InstagramWindow : Window
    {
        string myUserToken = "EAALYjhlBDZC4BAJx9tQci7xqIhmktN3pRSq9OZBLQ5wsTZA7hMJ3Jib0JuEFckqk43I5tY9UphNzmsoL9SeSsjcTMv9JJC8YPBtLxtuZCV5ixppxjJ5HnUfKa0jdXsrsuMxmNkfuQbJ2sSM6sNjA8LxwxZA3hIy86LJD7wyjGqC2kA65me6xc";
        string myPageToken = "EAALYjhlBDZC4BAI1vYykNtgJMyxAhLEB9Mq8hxXf6x4YAqqe54g1jVSIjt79RDDLtFiByML0ND6GRPkY138xZAE0ymynRtZCZBTShd03QnxdH7EUXdzwRhH5H0GPaxIVEF8Lsz4QimuevZAxRJZCP0YimZBZBPJX6ZAQOldfqzKLBoREpgSiUAaxzFvum1MeI5njrr02GMOZCDtAZDZD";
        public InstagramWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            GetInstagramMedia();
        }

        private async void GetInstagramMedia()
        {
            InstagramManager manager = new InstagramManager(myUserToken);
            List<Media> results = await manager.GetMediaAsync();
        }
    }
}
