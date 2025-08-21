using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    private InputBindings _inputBindings;

    private void Start()
    {
        _inputBindings = new InputBindings();
    }

    private void Update()
    {
        HandleMovementInput();
        SendMessage();
        CubeSpawn();
    }

    private void HandleMovementInput()
    {
        Vector3 movementDirection = new Vector3(_inputBindings.Horizontal, 0, _inputBindings.Vertical);
        
        EventsProvider.GameplayEvents.OnMovementDirection?.Invoke(movementDirection);
    }


    private void SendMessage()
    {
        if (_inputBindings.Space)
        {
            EventsProvider.GameplayEvents.OnSendMessage?.Invoke();
        }
    }

    private void CubeSpawn()
    {
        if (_inputBindings.F)
        {
            EventsProvider.GameplayEvents.OnCubeSpawn?.Invoke();
        }
    }
}
