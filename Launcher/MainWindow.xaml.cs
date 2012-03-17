using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
namespace Launcher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Dictionary<string, List<string>> resolutionList;
        public MainWindow()
        {
            InitializeComponent();

            resolutionList = new Dictionary<string, List<string>>();
            resolutionList.Add("4:3", new List<string>());
            resolutionList["4:3"].Add("800x600");
            resolutionList["4:3"].Add("1024x768");

            resolutionList.Add("16:9", new List<string>());
            resolutionList["16:9"].Add("1280x720");
            resolutionList["16:9"].Add("1366x768");

            resolutionList.Add("16:10", new List<string>());
            resolutionList["16:10"].Add("1280×800");
            resolutionList["16:10"].Add("1440×900");
        }

        private void CencelButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void ComboBoxItem_Selected(object sender, RoutedEventArgs e)
        {

        }

        private void AspectRatio_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var val = AspectRatio.SelectedItem as ComboBoxItem;

            if (val.Content != null)
            {
                switch (val.Content.ToString())
                {
                    case "4:3":
                        ResolutionComboBox.ItemsSource = resolutionList["4:3"];
                        break;
                    case "16:9":
                        ResolutionComboBox.ItemsSource = resolutionList["16:9"];
                        break;
                    case "16:10":
                        ResolutionComboBox.ItemsSource = resolutionList["16:10"];
                        break;
                }
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Escape:
                    Application.Current.Shutdown();
                    break;
            }
        }

        private void RunGameButton_Click(object sender, RoutedEventArgs e)
        {
            Process gameProc;
            gameProc = Process.Start("Game.exe");
            Application.Current.Shutdown();
        }
    }
}
