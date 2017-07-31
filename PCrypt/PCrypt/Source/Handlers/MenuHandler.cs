namespace PCrypt.Source.Handlers
{
    using MahApps;
    using MahApps.Metro.Controls;

    class MenuHandler
    {
        private static HamburgerMenu _control;
        private static MenuHandler instance;

        private MenuHandler()
        {

        }

        public static MenuHandler Initialize(ref HamburgerMenu control)
        {
            if (instance == null)
            {
                instance = new MenuHandler();
                _control = control;
            }

            return instance;
        }

        public static void ChangeContent(object content)
        {
            _control.Content = content;
            _control.IsPaneOpen = false;
        }

        public static MenuHandler Instance { get => instance; private set => instance = value; }
        public static HamburgerMenu Menu { get => _control; private set => _control = value; }
    }
}
