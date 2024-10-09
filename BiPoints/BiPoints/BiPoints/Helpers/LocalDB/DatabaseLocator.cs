using BiPoints.Services.LocalDB;
using System;

namespace BiPoints.Helpers.LocalDB
{
    class DatabaseLocator
    {
        public static LocalDb LocalDb { get; set; }
        public static Guid Id { get; set; }
        public static Guid UserId { get; set; }
        public static string Token { get; set; }
    }
}
