using System.Collections.Generic;
using System.Windows.Controls;

namespace PCrypt.Controls
{
    using PCrypt.Source.Enums;
    using System.Diagnostics;
    using System.IO;
    using System.Windows;

    /// <summary>
    /// Interaction logic for SelectedFileControl.xaml
    /// </summary>
    public partial class SelectedFileControl : UserControl
    {
        private List<string> selectedFiles = new List<string>();
        private List<string> fileFilter = new List<string>();
        private FileViewMode viewMode;
        private FILTER_FILE_MODE filterMode;

        public SelectedFileControl()
        {
            InitializeComponent();

            this.viewMode = FileViewMode.LINES;
            this.filterMode = FILTER_FILE_MODE.NOT_IF_CONTAINS;

            UpdateView();
        }

        public void AddFile(string fpath)
        {
            if (!selectedFiles.Contains(fpath))
            {
                switch (filterMode)
                {
                    case FILTER_FILE_MODE.ADD_IF_CONTAINS:
                        if (fileFilter.Contains(Path.GetExtension(fpath)))
                        {
                            selectedFiles.Add(fpath);
                            UpdateView();
                        }
                    break;

                    case FILTER_FILE_MODE.NOT_IF_CONTAINS:
                        if (!fileFilter.Contains(Path.GetExtension(fpath)))
                        {
                            selectedFiles.Add(fpath);
                            UpdateView();
                        }
                    break;
                }
            }
        }

        public void AddFiles(string[] fpaths)
        {
            for (int i = 0; i < fpaths.Length; i++)
            {
                string fpath = fpaths[i];

                if (!selectedFiles.Contains(fpath) && File.Exists(fpath))
                {
                    switch (filterMode)
                    {
                        case FILTER_FILE_MODE.ADD_IF_CONTAINS:
                            if (fileFilter.Contains(Path.GetExtension(fpath)))
                            {
                                selectedFiles.Add(fpath);
                            }
                        break;

                        case FILTER_FILE_MODE.NOT_IF_CONTAINS:
                            if (!fileFilter.Contains(Path.GetExtension(fpath)))
                            {
                                selectedFiles.Add(fpath);
                                
                            }
                        break;
                    }
                }
            }

            UpdateView();
        }

        public void RemoveFileByName(string fpath)
        {
            selectedFiles.Remove(fpath);
            UpdateView();
        }

        public void ClearAll()
        {
            selectedFiles.Clear();
            UpdateView();
        }

        private void UpdateView()
        {
            if (selectedFiles.Count < 1)
            {
                DropFileControl.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                DropFileControl.Visibility = System.Windows.Visibility.Hidden;
            }

            FileContainer.Children.Clear();
            selectedFiles.ForEach(x =>
            {
                FileContainer.Children.Add(new FileItemControl(x, this));
            });
        }

        public void RemoveFileByControl(FileItemControl item)
        {
            RemoveFileByName(item.FPath);
        }

        public List<string> SelectedFiles { get => selectedFiles; }

        private void OnFileDrop(object sender, System.Windows.DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                for (int i = 0; i < files.Length; i++)
                {
                    AddFile(files[i]);
                }
            }
        }

        /// <summary>
        /// Add a filter
        /// </summary>
        /// <param name="filter">E.g. ".jpg"</param>
        public void AddFilter(string filter)
        {
            if (!fileFilter.Contains(filter))
            {
                fileFilter.Add(filter);
            }
        }

        /// <summary>
        /// Remove a filter
        /// </summary>
        /// <param name="filter">E.g. ".jpg"</param>
        public void RemoveFilter(string filter)
        {
            if (fileFilter.Contains(filter))
            {
                fileFilter.Remove(filter);
            }
        }

        public FILTER_FILE_MODE FilterMode { get => filterMode; set => filterMode = value; }
    }

    public enum FILTER_FILE_MODE
    {
        ADD_IF_CONTAINS,
        NOT_IF_CONTAINS,
    }
}
