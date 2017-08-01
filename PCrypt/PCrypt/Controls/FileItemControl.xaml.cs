using PCrypt.Source.Filesystem;
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

namespace PCrypt.Controls
{
    /// <summary>
    /// Interaction logic for FileItemControl.xaml
    /// </summary>
    public partial class FileItemControl : UserControl
    {
        private string fpath;
        private SelectedFileControl parent;

        public FileItemControl(string fpath, SelectedFileControl parent)
        {
            InitializeComponent();

            this.fpath = fpath;
            this.parent = parent;
            this.PART_NAME.Text = fpath;
        }

        private void OnDeleteClicked(object sender, RoutedEventArgs e)
        {
            parent.RemoveFileByControl(this);
        }

        private void OnOpenFolderClicked(object sender, RoutedEventArgs e)
        {
            FileHandler.OpenFolderWin32(fpath);
        }

        public string FPath { get => fpath; }
    }
}
