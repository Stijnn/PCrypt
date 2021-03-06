﻿namespace PCrypt.Source.Handlers
{
    using System.Windows.Controls;

    class OverlayHandler
    {
        private static Grid _control;
        private static OverlayHandler instance;
        private static Views.LoadingView loadview;

        private OverlayHandler()
        {

        }

        public static OverlayHandler Initialize(ref Grid control)
        {
            if (instance == null)
            {
                instance = new OverlayHandler();
                loadview = new Views.LoadingView();
                _control = control;
            }

            return instance;
        }

        public static void OpenOverlay(UserControl view)
        {
            _control.Visibility = System.Windows.Visibility.Visible;
            _control.Children.Add(view);
            Panel.SetZIndex(_control, 1);
        }

        public static void HideOverlay()
        {
            _control.Visibility = System.Windows.Visibility.Hidden;
            _control.Children.Clear();
            Panel.SetZIndex(_control, -1);
        }

        public static void ShowLoadingOverlay()
        {
            OpenOverlay(loadview);
        }

        public static void RemoveLoadingOverlay()
        {
            _control.Children.Remove(loadview);
        }

        public static OverlayHandler Instance { get => instance; private set => instance = value; }
    }
}
