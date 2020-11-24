using DiscordRPC;
using DiscordRPC.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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

namespace MavenFNRewrite
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public DiscordRpcClient client;

        private DiscordRpc.RichPresence presence;
        private DiscordRpc.EventHandlers handlers;

        MavenFN.Fortnite maven = new MavenFN.Fortnite();
        public MainWindow()
        {
            InitializeComponent();

            client = new DiscordRpcClient("777890793095692298");
            client.Logger = new ConsoleLogger() { Level = LogLevel.Warning };
            client.Initialize();
            client.SetPresence(new DiscordRPC.RichPresence()
            {
                Details = "MavenFN-Fortnite",
                State = "discord.gg/uCp9mvVzCa",
                Timestamps = Timestamps.Now,
                Assets = new Assets()
                {
                    LargeImageKey = "capture",
                    LargeImageText = "MavenFN",
                    SmallImageKey = "capture",
                }
            });


            //this.handlers = new DiscordRpc.EventHandlers();
            //DiscordRpc.Initialize("777890793095692298", ref this.handlers, true, null);
            //this.presence.details = "MavenFN-Fortnite" ;
            //this.presence.state = "[ Maven Official Users ]";
            //this.presence.largeImageKey = "capture";
            //this.presence.largeImageText = "MavenPic";
            //DiscordRpc.UpdatePresence(ref this.presence);
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainBorder_Copy1.Visibility = Visibility.Visible;
        }

        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern ushort GlobalAddAtom(string lpString);

        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        static extern ushort GlobalDeleteAtom(ushort nAtom);

        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern ushort GlobalFindAtom(string lpString);

        private void MouseAimbot_Checked(object sender, RoutedEventArgs e)
        {

            if (MouseAimbot.IsChecked == true)
            {
                if (GlobalFindAtom("Memory") == 0)
                    GlobalAddAtom("Memory");
            }
            else
            {
                string bak = @"C:\Maven\Memory.txt";
                string FOV = MouseAimbot.ToString();
                string[] lines = { FOV };
                System.IO.File.WriteAllLines(bak, lines);
                if (GlobalFindAtom("Memory") == 0)
                    GlobalAddAtom("Memory");
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            maven.MavenAttach();
        }

        private void Memory_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void PlayerEsp_Checked(object sender, RoutedEventArgs e)
        {
            if (PlayerEsp.IsChecked == true)
            {
                if(GlobalFindAtom("Box") == 0)
                    GlobalAddAtom("Box");
            }
            else
            {
                string bak = @"C:\Maven\Box.txt";
                string FOV = PlayerEsp.ToString();
                string[] lines = { FOV };
                System.IO.File.WriteAllLines(bak, lines);
                if (GlobalFindAtom("Box") == 0)
                    GlobalAddAtom("Box");
            }
        }

        private void PlayersName_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void LootEsp_Checked(object sender , RoutedEventArgs e)
        {

        }

        private void Llama_Checked(object sender , RoutedEventArgs e)
        {

        }

        private void Silent_Checked(object sender , RoutedEventArgs e)
        {

        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            string bak = @"C:\Maven\settings.bak";
            string FOV = Slide.Value.ToString();
            string[] lines = { FOV };
            System.IO.File.WriteAllLines(bak, lines);
            if (GlobalFindAtom("FOV") == 0)
                GlobalAddAtom("FOV");
        }
    }
}
