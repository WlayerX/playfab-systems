using Core.Domain.Repositories;
using PlayFab;
using PlayFab.ClientModels;

namespace Infra.PlayFab {
    public class PlayFabAuthRepository : IAuthRepository {
        public void Login(string username, string password) {
            var request = new LoginWithPlayFabRequest { Username = username, Password = password };
            PlayFabClientAPI.LoginWithPlayFab(request, result => {
                UnityEngine.Debug.Log($"Logged in as {result.PlayFabId}");
            }, error => {
                UnityEngine.Debug.LogError(error.GenerateErrorReport());
            });
        }
    }
}
