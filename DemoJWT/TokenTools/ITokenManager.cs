using DemoJWT.Entities;

namespace DemoJWT.TokenTools
{
    public interface ITokenManager
    {
        User Authentitcate(User user);
    }
}