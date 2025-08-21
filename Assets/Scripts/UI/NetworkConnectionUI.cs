using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NetworkConnectionUI : MonoBehaviour
{
    [Header("Setting")]
    [SerializeField] private Button _startHostButton;
    [SerializeField] private Button _startClientButton;
    [SerializeField] private TMP_InputField _nickmane;
    
    
    [Header("Nickname Settings")]
    [SerializeField] private string[] _adjectives = { "Dark", "Epic", "Swift", "Mighty", "Stealthy", "Cosmic" };
    [SerializeField] private string[] _nouns = { "Phoenix", "Wolf", "Eagle", "Tiger", "Dragon", "Falcon" };
    
    private void Start()
    {
        _startHostButton.onClick.AddListener(StartHost);
        _startClientButton.onClick.AddListener(StartClient);
    }

    private void StartHost()
    {
       SetNickname();
       EventsProvider.NetworkEvents.OnHostStarted?.Invoke();
    }

    private void StartClient()
    {
        SetNickname();
        EventsProvider.NetworkEvents.OnClientStarted?.Invoke();
    }
    
    private void SetNickname()
    {
        PlayerData.Nickname = !string.IsNullOrEmpty(_nickmane.text)
            ? _nickmane.text
            : GenerateNickname();
    }
    
    private string GenerateNickname()
    {
        string randomNickname = _adjectives[Random.Range(0, _adjectives.Length)] + 
                                _nouns[Random.Range(0, _nouns.Length)] + 
                                Random.Range(1, 100);

        return randomNickname;
    }
}
