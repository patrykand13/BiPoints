namespace BiPoints.BLL.DTO.Request.Authenticate
{
    public class RegisterRequest : LoginRequest
    {
        public string Name { get; set; }
        public string Lastname { get; set; }
    }
}
