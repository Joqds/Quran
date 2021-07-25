namespace Quran.Server.Infrastructure.Identity
{

    public static class JoqdsConstants
    {
        public const string Authority = "https://id.joqds.ir";

        public class Roles
        {
            public const string Guest = "Guest";
            public const string User = "User";
            public const string QuranAdmin = "QuranAdmin";
            public const string Admin = "Admin";
            public const string God = "God";
        }

        public static class ClaimTypes
        {
            public const string Version = "Version";
            public const string Role = "Role";
            public const string DisplayName = "DisplayName";
        }

        public static class Scope
        {
            public const string QuranNotif = "quran.notif";
        }

        public static class Grants
        {
            public const string Totp = "totp";
        }

        public static class ApiResources
        {
            public const string Notif = "notif";
            public const string Admin = "admin";
            public const string God = "god";
            public const string QuranApp = "quran";
            public const string QuranAdmin = "quranadmin";
        }

        public static class ClientPrefix
        {
            public const string QuranFlutterMobile = "QuranFlutterMobile";
            public const string QuranFlutterWeb = "QuranFlutterWeb";
            public const string QuranSwagger = "QuranSwagger";
            public const string QuranAdmin = "QuranAdmin";

        }
    }


}
