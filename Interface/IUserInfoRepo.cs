using System.Threading.Tasks;
using UnitPractical.Model;

namespace UnitPractical.Repository.Interface
{
    public interface IUserInfoRepo
    {
        Task<UserInfo> GetUserInfoByIdAsync(int id);
        Task<UserInfo> AddUserInfoAsync(UserInfo userInfo);
        Task<UserInfo> UpdateUserAsync(UserInfo userInfo);
        Task DeleteUserAsync(int id);
    }
}
