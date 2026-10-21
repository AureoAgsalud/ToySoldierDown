using UnityEngine;

[RequireComponent(typeof(PlayerMovementBehavior))]
public class PlayerJumpBehaviour : Interactor
{
    [SerializeField] private float _jumpVelocity = 10f;
    private PlayerMovementBehavior _playerMovementBehavior;

    void Start()
    {
        _playerMovementBehavior = GetComponent<PlayerMovementBehavior>();
    }
    public override void Interact()
    {
        if(PlayerInput.Instance.jumpPressed && _playerMovementBehavior.isGrounded)
        {
            _playerMovementBehavior.SetYVelocity(_jumpVelocity);
        }
    }
    
}
