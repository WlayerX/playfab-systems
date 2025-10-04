using System.Threading.Tasks;

namespace PlayfabClient.Modules {
    public interface IAuthService {
        Task<string> LoginWithCustomIdAsync(string customId);
    }
}
