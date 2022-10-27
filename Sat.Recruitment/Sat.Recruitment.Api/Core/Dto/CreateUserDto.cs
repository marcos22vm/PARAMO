namespace Sat.Recruitment.Api.Core.Dto
{
    public class CreateUserRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string UserType { get; set; }
        public string Money { get; set; }
    }

    public class CreateUserResponse
    {
        public bool IsSuccess { get; set; }
        public string Errors { get; set; }
    }
}
