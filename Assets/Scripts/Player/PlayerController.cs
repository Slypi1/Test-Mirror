using Mirror;
using TMPro;
using UnityEngine;

public class PlayerController : NetworkBehaviour
{
    [Header("PlayerSetting")]
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _offset;
    
    [SerializeField] private TMP_Text _nickname;
    [SerializeField] private Animator _animator;
    [SerializeField] private Rigidbody _rb;

    [Header("CameraSetting")] 
    [SerializeField] private Vector3 _localPosition;
    [SerializeField] private Vector3 _localRotation;
    
    [SyncVar]
    private string _syncNickname;
    
    [SyncVar(hook = nameof(OnRunStateChanged))]
    private bool _isRunning;

    private const string ANIM_RUN = "IsRun";
    
    public override void OnStartLocalPlayer()
    {

        base.OnStartLocalPlayer();
        
        Initialize();
    }
    
    public override void OnStartClient()
    {
        base.OnStartClient();
        
        UpdateNickname(_syncNickname);
    }
    
    private void Initialize()
    {
        EventsProvider.GameplayEvents.OnMovementDirection += Movement;
        EventsProvider.GameplayEvents.OnSendMessage += SendMessage;

        string newNickname = PlayerData.Nickname;
        
        CmdSetNickname(newNickname);
        UpdateNickname(newNickname);
        SetupCamera();
    }

    #region Nickname

    [Command]
    private void CmdSetNickname(string newNickname)
    {
        _syncNickname = newNickname;
        
        RpcUpdateNickname(newNickname);
    }
    
    [ClientRpc]
    private void RpcUpdateNickname(string newNickname)
    {
        if (!isLocalPlayer)
        {
            UpdateNickname(newNickname);
        }
    }
    
    private void UpdateNickname(string newNickname)
    {
        _nickname.text = newNickname;
    }
    
    #endregion

    #region Message

    private void SendMessage()
    {
        CmdSendMessageToAll();
    }
    
    [Command]
    public void CmdSendMessageToAll()
    {
        RpcReceiveMessage();
    }

    [ClientRpc]
    private void RpcReceiveMessage()
    {
        string message = $"Привет от {_syncNickname}";
        
        EventsProvider.GameplayEvents.OnDisplayMessage(message);
        Debug.Log(message);
    }
    #endregion
    
    private void Movement(Vector3 movementDirection)
    {
        if (!isLocalPlayer)
        {
            return;
        }
        
        Vector3 moveVelocity = movementDirection * _moveSpeed;
        _rb.velocity = moveVelocity;

        bool isMoving = movementDirection != Vector3.zero;
    
        _animator.SetBool(ANIM_RUN, isMoving);
    
        if (isMoving != _isRunning)
        {
            CmdSetRunState(isMoving);
        }

        if (movementDirection.magnitude > _offset)
        {
            RotateTowardsMovement(movementDirection);
        }
        
    }
    
    private void RotateTowardsMovement(Vector3 direction)
    {
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        
        transform.rotation = Quaternion.Slerp(
            transform.rotation, 
            targetRotation, 
            _rotationSpeed * Time.deltaTime
        );
    }
    
    private void OnRunStateChanged(bool oldValue, bool newValue)
    {
        if (!isLocalPlayer)
        {
            _animator.SetBool(ANIM_RUN, newValue);
        }
    }
    
    [Command]
    private void CmdSetRunState(bool isRunning)
    {
        _isRunning = isRunning;
    }
    
    private void SetupCamera()
    {
        Camera.main.transform.SetParent(transform);
        
        Camera.main.transform.localPosition = _localPosition;
        Camera.main.transform.localEulerAngles = _localRotation;
    }

    private void OnDestroy()
    {
        EventsProvider.GameplayEvents.OnMovementDirection -= Movement;
        EventsProvider.GameplayEvents.OnSendMessage -= SendMessage;
    }
}
