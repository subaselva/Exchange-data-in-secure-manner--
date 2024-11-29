using Application.DTDs;

using Application.DTDs;
namespace Application.Contracts
{
    public interface IUser
    {
        Task<RegistrationResponse> RegisterUserAsync(RegisterUserDTO registerUserDTO);
        Task<LoginResponce> LoginUserAsync(LoginDTO loginDTO);
        
    }
}
