namespace ApiLogin.Models.Response.Login
{
    public class LoginResponse
    {
        public string Token { get; set; }
        public DateTime ExpiresAt { get; set; }
        public string ErrorMessage { get; set; }
        public bool IsSuccess { get; set; }= false;
    }
    public class RegisterResponse
    {
        public int NewUserId { get; set; }
        public string ErrorMessage { get; set; }
    }
}
