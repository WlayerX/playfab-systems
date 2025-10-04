#if PLAYFAB
using System.Threading.Tasks;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

namespace PlayfabClient.Modules {
    public class PlayFabAuthService : IAuthService {
        public Task<string> LoginWithCustomIdAsync(string customId) {
            var tcs = new System.Threading.Tasks.TaskCompletionSource<string>();
            var req = new LoginWithCustomIDRequest { CustomId = customId, CreateAccount = true };
            PlayFabClientAPI.LoginWithCustomID(req,
                result => tcs.TrySetResult(result.PlayFabId),
                error => {
                    Debug.LogError($"PlayFab login failed: {error.GenerateErrorReport()}");
                    tcs.TrySetException(new System.Exception(error.GenerateErrorReport()));
                });
            return tcs.Task;
        }
    }
}
#endif
