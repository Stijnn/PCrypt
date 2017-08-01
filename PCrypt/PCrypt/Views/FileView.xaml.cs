using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Threading;

namespace PCrypt.Views
{
    using MahApps.Metro.Controls;
    using MahApps.Metro.Controls.Dialogs;
    using Microsoft.Win32;
    using PCrypt.Source.Cryptography;
    using PCrypt.Source.Handlers;
    using PCrypt.Source.Reporter;
    using PCrypt.Source.Structs;
    using System.Threading.Tasks;

    /// <summary>
    /// Interaction logic for FileView.xaml
    /// </summary>
    public partial class FileView : UserControl
    {
        public FileView()
        {
            InitializeComponent();
        }
    }
}
