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

namespace PCrypt
{
    using Microsoft.Win32;
    using Source.Cryptography;
    using System.Threading;
    using Source.Structs;
    using Source.Reporter;
    using MahApps.Metro.Controls;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            //string[] args = Environment.GetCommandLineArgs();
            //for (int i = 1; i < args.Length; i++)
            //{
            //    PFileCipher cipher = new PFileCipher();
            //    if (System.IO.Path.GetExtension(args[i]) == ".pcrypted")
            //        cipher.DecryptFile(args[i], "Hello World");
            //    else
            //        cipher.EncryptFile(args[i], "Hello World");
            //}

            InitializeComponent();


        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            //Setup previous settings
            if (Properties.Settings.Default.IsMorePage)
            {
                this.MENU_CONTROL.SelectedIndex = -1;
                this.MENU_CONTROL.SelectedOptionsIndex = Properties.Settings.Default.SelectedIndex;
            }
            else
                this.MENU_CONTROL.SelectedIndex = Properties.Settings.Default.SelectedIndex;

            //Setup ReporterView
            SReporter reporter = SReporter.Create(PROGRESS_VIEW);
            SReporter.SetIsIntermediate(false);
            SReporter.SetReporter("CURRENT TASK: ");
            SReporter.SetStatus("WAITING");
        }

        private void OnClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Properties.Settings.Default.SelectedIndex = this.MENU_CONTROL.SelectedIndex == -1 ? this.MENU_CONTROL.SelectedOptionsIndex : this.MENU_CONTROL.SelectedIndex;
            Properties.Settings.Default.IsMorePage = this.MENU_CONTROL.SelectedIndex == -1 ? true : false;

            Properties.Settings.Default.Save();
        }

        private void OnMenuItemClicked(object sender, MahApps.Metro.Controls.ItemClickEventArgs e)
        {
            // set the content
            this.MENU_CONTROL.Content = e.ClickedItem;
            // close the pane
            this.MENU_CONTROL.IsPaneOpen = false;
        }
    }
}
