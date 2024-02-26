using EYProveedores.Models.Custom;
namespace EYProveedores.Services
{
    public interface IAuthorizationService
    {
        Task<AuthorizationResponse> DevolverToken(AuthorizationRequest authorization);
    }
}
