using System.Windows;
using System.Windows.Controls;

namespace PCrypt.Views
{
    using Microsoft.Win32;
    using PCrypt.Source.Cryptography;
    using PCrypt.Source.Structs;
    using System.Threading;

    /// <summary>
    /// Interaction logic for FileView.xaml
    /// </summary>
    public partial class FileView : UserControl
    {
        public FileView()
        {
            InitializeComponent();
        }

        private void OnFileSelection(object sender, RoutedEventArgs e)
        {
            OpenFileDialog wnd = new OpenFileDialog();

            wnd.CheckFileExists = true;
            wnd.Multiselect = true;

            if (wnd.ShowDialog() == true)
            {
                //APP_GRID.IsEnabled = false;

                string[] files = wnd.FileNames;
                for (int i = 0; i < files.Length; i++)
                {
                    if (!string.IsNullOrWhiteSpace(""))
                    {
                        ThreadPool.QueueUserWorkItem(new WaitCallback(ProcessFile), new PCryptFileInfo(files[i], ""));
                    }
                }

                //APP_GRID.IsEnabled = true;
            }
        }

        private void ProcessFile(object info)
        {
            if (info.GetType() == typeof(PCryptFileInfo))
            {
                PCryptFileInfo pinfo = info as PCryptFileInfo;
                PFileCipher cipher = new PFileCipher();

                if (System.IO.Path.GetExtension(pinfo.FPath) == ".pcrypted")
                    cipher.DecryptFile(pinfo.FPath, pinfo.PassKey);
                else
                    cipher.EncryptFile(pinfo.FPath, pinfo.PassKey);
            }
        }
    }
}
