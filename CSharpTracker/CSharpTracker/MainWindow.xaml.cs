using CSharpTracker.Facebook.Controller;
using Facebook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CSharpTracker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            FacebookClient fbClient = new FacebookClient
            {
                AppId = "AppIdHere",
                AppSecret = "AppSecretHere",
                AccessToken = "AccessTokenHere"
            };
            //new FacebookPost(fbClient).PostToWall("103967058170662");
            //new FacebookPost(fbClient).GetPostComments("103967058170662_105535241347177");
            //new FacebookPage(fbClient).GetPageProperty();
            //103967058170662_105535241347177
            //new FacebookPost(fbClient).UpdatePost("103967058170662_105535241347177"); NOT YET
            //new FacebookPost(fbClient).UploadPhoto("103967058170662");
            //103967058170662_105535241347177/insights?metric=post_reactions_like_total,post_reactions_love_total

            //new FacebookPost(fbClient).GetPostReactions("103967058170662_105535241347177");

        }
    }
}
