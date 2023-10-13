namespace BiPoints.BLL.DTO.Request
{
    public class RegisterRequest : LoginRequest
    {
        public string Name { get; set; }
        public string Lastname { get; set; }
    }
}
