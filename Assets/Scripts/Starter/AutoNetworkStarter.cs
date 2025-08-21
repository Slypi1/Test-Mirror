using System.Collections;
using Mirror;
using UnityEngine;

public class AutoNetworkStarter : MonoBehaviour
{
    [SerializeField] private NetworkManager _networkManager;
    [SerializeField] private float _delay;
    
    private const string GAME_SCENE = "Game";
    
    private void Start()
    {
        EventsProvider.NetworkEvents.OnHostStarted += StartHost;
        EventsProvider.NetworkEvents.OnClientStarted += StartClient;
    }

    private void StartHost()
    {
        _networkManager.StartHost();
        
        StartCoroutine(ChangeSceneAfterDelay());
        Dispose();
    }

    private IEnumerator ChangeSceneAfterDelay()
    {
        yield return new WaitForSeconds(_delay);
        
        _networkManager.ServerChangeScene(GAME_SCENE);
    }
    
    private void StartClient()
    {
        _networkManager.StartClient();
        
        Dispose();
    }
    
    private void Dispose()
    {
        EventsProvider.NetworkEvents.OnHostStarted -= StartHost;
        EventsProvider.NetworkEvents.OnClientStarted -= StartClient;
    }
}
