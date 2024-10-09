namespace BiPoints.Models.Request.Authentication
{
    public class RegisterRequest : LoginRequest
    {
        public string Name { get; set; }
        public string Lastname { get; set; }
    }
}
