using RestWithAspNet5.Data.VO;
using RestWithAspNet5.Model;

namespace RestWithAspNet5.Repository
{
    public interface IUserRepository
    {
        User ValidateCredentials(UserVO userVO);
        User RefreshUserInfo(User user)
    }
}
