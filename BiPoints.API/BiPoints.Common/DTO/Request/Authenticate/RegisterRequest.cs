namespace BiPoints.Common.DTO.Request.Authenticate
{
    public class RegisterRequest : LoginRequest
    {
        public string Name { get; set; }
        public string Lastname { get; set; }
    }
}
