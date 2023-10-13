namespace BiPoints.BLL.DTO.Response.User
{
    internal class PersonalUserResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string Language { get; set; }
        public int PhoneNumber { get; set; }
    }
}
