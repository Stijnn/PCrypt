namespace PCrypt.Source.Handlers
{


    class UserHandler
    {
        private static PCrypt.Source.Structs.PCryptUser uuid;
        private static UserHandler instance;

        private UserHandler()
        {

        }

        public static AUTH_RESULT Authenticate(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(uuid))
            {

            }
            else
                return AUTH_RESULT.Failed;
        }

        public static void Logout()
        {
            uuid = null;
        }
    }

    enum AUTH_RESULT { Succesfull, Failed }
}
