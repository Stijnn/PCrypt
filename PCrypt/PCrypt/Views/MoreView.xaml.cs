using System.Windows;
using System.Windows.Controls;

namespace PCrypt.Views
{
    using MahApps.Metro.Controls;
    using PCrypt.Source.Handlers;
    using System.Threading;

    /// <summary>
    /// Interaction logic for MoreView.xaml
    /// </summary>
    public partial class MoreView : UserControl
    {
        public MoreView()
        {
            InitializeComponent();
        }

        private void OnSettingsClicked(object sender, RoutedEventArgs e)
        {
            OverlayHandler.OpenOverlay(new SettingsView());
        }

        private void OnAccountClicked(object sender, RoutedEventArgs e)
        {
            
        }

        private void OnAboutClicked(object sender, RoutedEventArgs e)
        {

        }
    }
}
