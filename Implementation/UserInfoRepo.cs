using Microsoft.Azure.Cosmos;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using UnitPractical.Context;
using UnitPractical.Model;
using UnitPractical.Repository.Interface;

namespace UnitPractical.Repository.Implementation
{
    public class UserInfoRepo : IUserInfoRepo
    {
        private readonly AppDBContext _context;

        public UserInfoRepo(AppDBContext context)
        {
            _context = context;
        }

        public async Task<UserInfo> GetUserInfoByIdAsync(int id)
        {
            return await _context.UserInfos.FirstOrDefaultAsync(x => x.ID == id);
        }

        public async Task<UserInfo> AddUserInfoAsync(UserInfo userInfo)
        {
            _context.UserInfos.Add(userInfo);
            await _context.SaveChangesAsync();

            return userInfo;
        }

        public async Task<UserInfo> UpdateUserAsync(UserInfo userInfo)
        {
            _context.UserInfos.Update(userInfo);
            await _context.SaveChangesAsync();
            return userInfo;
        }

        public async Task DeleteUserAsync(int id)
        {
            var userInfo = await _context.UserInfos.FindAsync(id);
            if (userInfo != null)
            {
                _context.UserInfos.Remove(userInfo);
                await _context.SaveChangesAsync();
            }
        }
    }
}
