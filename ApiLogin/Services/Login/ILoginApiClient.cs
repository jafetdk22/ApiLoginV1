using ApiLogin.Models.Request.Login;
using ApiLogin.Models.Response.Login;

namespace ApiLogin.Services.Login
{
    public interface ILoginApiClient
    {
        Task<LoginResponse> PostLogin(LoginRequest request);
        Task<RegisterResponse> PostUser(RegisterRequest request);
    }
}
