using PigVisionMobile.Core.Models.User;
using System.Threading.Tasks;

namespace PigVisionMobile.Core.Services.User
{
    public interface IUserService
    {
        Task<UserInfo> GetUserInfoAsync(string authToken);
    }
}
