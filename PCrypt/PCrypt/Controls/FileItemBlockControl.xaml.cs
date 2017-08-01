using System;
using System.Collections.Generic;
using System.IO;
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

namespace PCrypt.Controls
{
    /// <summary>
    /// Interaction logic for FileItemBlockControl.xaml
    /// </summary>
    public partial class FileItemBlockControl : UserControl
    {
        private string fpath;

        public FileItemBlockControl(string fpath)
        {
            InitializeComponent();

            this.fpath = fpath;
            txtName.Text = PathToValidFormat(fpath);
        }

        private string PathToValidFormat(string path)
        {
            return Path.GetFileName(path).Take(10).ToString();
        }
    }
}
