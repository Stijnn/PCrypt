using System.Collections.Generic;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

namespace PCrypt.Views.FileViewViews
{
    using MahApps.Metro.Controls;
    using MahApps.Metro.Controls.Dialogs;
    using Microsoft.Win32;
    using PCrypt.Source.Cryptography;
    using PCrypt.Source.Handlers;
    using PCrypt.Source.Reporter;
    using PCrypt.Source.Structs;
    using System.Windows.Media;

    /// <summary>
    /// Interaction logic for EncryptorFileView.xaml
    /// </summary>
    public partial class EncryptorFileView : UserControl
    {
        public EncryptorFileView()
        {
            InitializeComponent();
        }

        private async void EncryptFilesAsync(object a)
        {
            OverlayHandler.ShowLoadingOverlay();

            string pass = string.Empty;
            await Application.Current.Dispatcher.Invoke(async () =>
            {
                var hwnd = (Application.Current.MainWindow as MetroWindow);
                pass = await hwnd.ShowInputAsync("Please enter a password", "", new MetroDialogSettings()
                {
                    AffirmativeButtonText = "Continue",
                });
            });

            if (!string.IsNullOrWhiteSpace(pass))
            {
                List<string> files = SelectionView.SelectedFiles;

                SReporter.ResetProgress();
                SReporter.SetMaxValue(files.Count);
                SReporter.SetStatus("ENCRYPTING");

                int amountOfThreads = files.Count;
                using (CountdownEvent countdown = new CountdownEvent(amountOfThreads))
                {
                    for (int i = 0; i < amountOfThreads; i++)
                    {
                        ThreadPool.QueueUserWorkItem(x =>
                        {
                            ProcessFileAsync(x);
                            countdown.Signal();
                        }, new PCryptFileInfo(files[i], pass));
                    }

                    countdown.Wait();
                    countdown.Dispose();
                }

                SReporter.SetStatus("COMPLETED");
            }
            else
            {
                await Application.Current.Dispatcher.Invoke(async () =>
                {
                    var hwnd = (Application.Current.MainWindow as MetroWindow);
                    await hwnd.ShowMessageAsync("Failed", "No password supplied");
                });

                SReporter.SetStatus("FAILED");
            }

            OverlayHandler.RemoveLoadingOverlay();
        }

        private async void ProcessFileAsync(object info)
        {
            if (info.GetType() == typeof(PCryptFileInfo))
            {
                PCryptFileInfo pinfo = info as PCryptFileInfo;
                PFileCipher cipher = new PFileCipher();
                cipher.EncryptFile(pinfo.FPath, pinfo.PassKey);
                SReporter.UpdateValue(1);
            }
        }

        private void OnEcnryptFiles(object sender, RoutedEventArgs e)
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(EncryptFilesAsync), null);
        }

        private void OnAddFile(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileSelector = new OpenFileDialog();
            fileSelector.CheckFileExists = true;
            fileSelector.CheckPathExists = true;
            fileSelector.Multiselect = true;

            if (fileSelector.ShowDialog() == true)
            {
                string[] files = fileSelector.FileNames;

                if (fileSelector.FileNames.Length > 0)
                {
                    ThreadPool.QueueUserWorkItem(new WaitCallback(AddFilesAsync), files);
                }
                
                txtCount.Foreground = Brushes.White;
                txtCount.Text = SelectionView.FileContainer.Children.Count.ToString();
            }
        }

        private async void AddFilesAsync(object obj)
        {
            if (obj.GetType() == typeof(string[]))
            {
                SelectionView.AddFiles(obj as string[]);
            }
        }
    }
}
