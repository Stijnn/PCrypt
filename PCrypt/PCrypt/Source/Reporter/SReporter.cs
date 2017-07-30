namespace PCrypt.Source.Reporter
{
    using System.Windows.Media;

    public class SReporter
    {
        private static SReporter instance;
        private static ProgressView view;

        private SReporter()
        {

        }

        public static void SetStatus(string status)
        {
            view.UpdateStatus(status);
        }

        public static void SetReporter(string text)
        {
            view.UpdateReporter(text);
        }

        public static void SetIsIntermediate(bool enabled)
        {
            view.ChangeItermediate(enabled);
        }

        public static void SetMaxValue(int val)
        {
            view.SetMaxValue(val);
        }

        public static void UpdateValue(int val)
        {
            view.UpdateValueWith(val);
        }

        public static void ResetProgress()
        {
            view.ResetProgress();
        }

        public static void SetColor(SolidColorBrush brush)
        {
            view.ChangeColor(brush);
        }

        public static SReporter Create(ProgressView reportView)
        {
            if (instance == null)
            {
                instance = new SReporter();
                view = reportView;
            }

            return instance;
        }

        /// <summary>
        /// You need to Create() the instance first else it will return NULL
        /// </summary>
        public static SReporter Instance { get => instance; private set => instance = value; }
    }
}
