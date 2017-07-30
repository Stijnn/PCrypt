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
    /// <summary>
    /// Interaction logic for ProgressView.xaml
    /// </summary>
    public partial class ProgressView : UserControl
    {
        public ProgressView()
        {
            InitializeComponent();

            progressBar.Minimum = 0;
        }

        public void UpdateStatus(string status)
        {
            lblAction.Text = status;
        }

        public void UpdateReporter(string text)
        {
            lblReporter.Text = text;
        }

        public void ChangeItermediate(bool enabled)
        {
            progressBar.IsIndeterminate = enabled;
        }

        public void ChangeColor(SolidColorBrush brush)
        {
            progressBar.Foreground = brush;
        }

        public void SetMaxValue(int val)
        {
            progressBar.Maximum = val;
        }

        public void SetValue(int val)
        {
            progressBar.Value = val;
        }

        public void ResetProgress()
        {
            progressBar.Value = progressBar.Minimum;
        }

        public void UpdateValueWith(int val)
        {
            progressBar.Value += val;
        }
    }
}
