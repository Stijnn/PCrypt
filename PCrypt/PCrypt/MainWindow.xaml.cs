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

        private void OnFileSelection(object sender, RoutedEventArgs e)
        {
            OpenFileDialog wnd = new OpenFileDialog();

            wnd.CheckFileExists = true;
            wnd.Multiselect = true;

            if (wnd.ShowDialog() == true)
            {
                APP_GRID.IsEnabled = false;

                string[] files = wnd.FileNames;
                for (int i = 0; i < files.Length; i++)
                {
                    if (!string.IsNullOrWhiteSpace(""))
                    {
                        ThreadPool.QueueUserWorkItem(new WaitCallback(ProcessFile), new PCryptFileInfo(files[i], ""));
                    }
                }

                APP_GRID.IsEnabled = true;
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

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            SReporter reporter = SReporter.Create(PROGRESS_VIEW);
            SReporter.SetIsIntermediate(false);
            SReporter.SetReporter("CURRENT TASK: ");
            SReporter.SetStatus("WAITING");
        }
    }
}
