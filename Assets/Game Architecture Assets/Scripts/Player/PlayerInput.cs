using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-100)]
public class PlayerInput : MonoBehaviour
{
    public static PlayerInput Instance { get; private set; }    
    public float horizontalInput { get; private set; }
    public float verticalInput { get; private set; }
    public float mouseX { get; private set; }
    public float mouseY { get; private set; }


    public bool sprintHeld { get; private set; }
    public bool jumpPressed { get; private set; }
    public bool activatePressed { get; private set; }
    public bool primaryShootPressed { get; private set; }
    public bool secondaryShootPressed { get; private set; }
    public bool alpha1Pressed { get; private set; }
    public bool alpha2Pressed { get; private set; }
     
    public bool escapePressed { get; private set; }

    [Header("InputActions")]
    [SerializeField] InputActionReference moveAction;
    [SerializeField] InputActionReference lookAction;
    [SerializeField] InputActionReference sprintAction;
    [SerializeField] InputActionReference jumpAction;
    [SerializeField] InputActionReference activateAction;
    [SerializeField] InputActionReference primaryShootAction;
    [SerializeField] InputActionReference secondaryShootAction;

    // - Strategy Pattern - allows for different types of shooting
    [SerializeField] InputActionReference alpha1Action;
    [SerializeField] InputActionReference alpha2Action;
    [SerializeField] InputActionReference escapeAction;
    private bool clear;

    private void Awake()
    {
        // - Singleton Pattern - densures only one instance of PlayerInput exists
        if(Instance == null)
        {
            Instance = this;
        }

        // - Singleton Pattern - Destroy any other copies of our singleton class reference
        else if (Instance != null)
        {
            Destroy(this.gameObject);
        }
        
    }
    private void OnEnable()
    {
        moveAction.action.Enable();
        lookAction.action.Enable();
        sprintAction.action.Enable();
        jumpAction.action.Enable();
        activateAction.action.Enable();
        primaryShootAction.action.Enable();
        secondaryShootAction.action.Enable();

        alpha1Action.action.Enable();
        alpha2Action.action.Enable();
        escapeAction.action.Enable();
    }
    private void OnDisable()
    {
        moveAction.action.Disable();
        lookAction.action.Disable();
        sprintAction.action.Disable();
        jumpAction.action.Disable();
        activateAction.action.Disable();
        primaryShootAction.action.Disable();
        secondaryShootAction.action.Disable();

        alpha1Action.action.Disable();
        alpha2Action.action.Disable();
        escapeAction.action.Disable();
    }


    void Update()
    {
        ClearInputs();
        ProcessInputs();
    }
    private void ProcessInputs()
    {
        escapePressed |= escapeAction.action.WasPressedThisFrame();

        // - Stops Input when Paused
        if (GameManager.instance.currentGameState != GameManager.GameState.GamePlaying) return;

        Vector2 move = moveAction.action.ReadValue<Vector2>();
        Vector2 look = lookAction.action.ReadValue<Vector2>();

        horizontalInput = move.x;
        verticalInput = move.y;

        mouseX = look.x;
        mouseY = look.y;

        sprintHeld = sprintAction.action.IsPressed();

        jumpPressed |= jumpAction.action.WasPressedThisFrame();
        activatePressed |= activateAction.action.WasPressedThisFrame();

        primaryShootPressed |= primaryShootAction.action.WasPressedThisFrame();
        secondaryShootPressed |= secondaryShootAction.action.WasPressedThisFrame();

        alpha1Pressed |= alpha1Action.action.WasPressedThisFrame();
        alpha2Pressed |= alpha2Action.action.WasPressedThisFrame();

        escapePressed |= escapeAction.action.WasPressedThisFrame();
    }
    private void LateUpdate()
    {
        clear = true;
    }
    private void ClearInputs()
    {
        if (!clear)
            return;

        horizontalInput = 0;
        verticalInput = 0;
        mouseX = 0;
        mouseY = 0;

        sprintHeld = false;
        jumpPressed = false;
        activatePressed = false;

        primaryShootPressed = false;
        secondaryShootPressed = false;

        alpha1Pressed = false;
        alpha2Pressed = false;

        escapePressed = false;
        clear = false;
    }
}