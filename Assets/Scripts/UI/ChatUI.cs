using TMPro;
using UnityEngine;

public class ChatUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _message;

    private void Start()
    {
        EventsProvider.GameplayEvents.OnDisplayMessage += DisplayMessage;
    }

    private void DisplayMessage(string message)
    {
        _message.text = message;
    }

    private void OnDestroy()
    {
        EventsProvider.GameplayEvents.OnDisplayMessage -= DisplayMessage;
    }
}
