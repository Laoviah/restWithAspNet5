using RestWithAspNet5.Data.VO;

namespace RestWithAspNet5.Business
{
    public interface ILoginBusiness
    {
        TokenVO ValidateCredentials(UserVO user);
    }
}
