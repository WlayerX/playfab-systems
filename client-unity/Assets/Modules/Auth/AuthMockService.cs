using System.Threading.Tasks;
using UnityEngine;

namespace PlayfabClient.Modules {
    public class AuthMockService : IAuthService {
        public async Task<string> LoginWithCustomIdAsync(string customId) {
            await Task.Yield();
            Debug.Log($"[AuthMockService] Login called with {customId}");
            return \"MOCK_PLAYFAB_ID_\" + customId;
        }
    }
}
