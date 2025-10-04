using UnityEngine;
using PlayfabClient.Modules;
using Core.Infra;

public class Bootstrap : MonoBehaviour {
    void Awake() {
        // Simple bootstrap example: register mocks or real services by define symbols
#if PLAYFAB
        ServiceLocator.Register< IAuthService >( new PlayFabAuthService() );
#else
        ServiceLocator.Register< IAuthService >( new AuthMockService() );
#endif
    }
}
