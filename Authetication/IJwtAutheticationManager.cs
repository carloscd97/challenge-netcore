using BcpChallenge.ViewModels;

namespace BcpChallenge.Authetication
{
    public interface IJwtAutheticationManager
    {
        UserAuthenticatedViewModel Autheticate(string username, string password);
    }
}
