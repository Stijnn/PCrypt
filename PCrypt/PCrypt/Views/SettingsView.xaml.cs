namespace PCrypt.Views
{
    using PCrypt.Source.Handlers;
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    /// Interaction logic for SettingsView.xaml
    /// </summary>
    public partial class SettingsView : UserControl
    {
        public SettingsView()
        {
            InitializeComponent();
        }

        private void OnReturn(object sender, RoutedEventArgs e)
        {
            OverlayHandler.HideOverlay();
        }
    }
}
