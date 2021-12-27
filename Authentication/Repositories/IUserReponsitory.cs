using BookStore.Data.Entities;

namespace BookStore.Data.Repositories
{
    public interface IUserReponsitory
    {
        User GetByUserNameAndPassWord(string userName, string passWord);
        User GetUserByGoogleIdentifier(string googleId);
    }

}
