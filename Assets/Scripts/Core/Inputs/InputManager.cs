using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class InputManager : Singleton<InputManager>
{
    public PlayerControls controls;

    // Events
    public event Action<Vector2> OnMove;
    public event Action OnInteract;
    public event Action OnInteractCanceled;
    public event Action OnPause;
    public event Action OnBack;

    protected override void Awake()
    {
        base.Awake();

        controls = new PlayerControls();
    }

    private void OnEnable()
    {
        controls.Player.Enable();

        // Movement
        controls.Player.Move.performed += ctx => OnMove?.Invoke(ctx.ReadValue<Vector2>());
        controls.Player.Move.canceled += ctx => OnMove?.Invoke(Vector2.zero);

        // Interaction
        controls.Player.Interact.performed += ctx => OnInteract?.Invoke();
        controls.Player.Interact.canceled += ctx => OnInteractCanceled?.Invoke();

        // Pause
        controls.Player.Pause.performed += ctx => OnPause?.Invoke();

        //Back
        controls.Player.Back.performed += ctx => OnBack?.Invoke();
    }

    private void OnMovePerformed(Vector2 vector)
    {

    }

    private void OnDisable()
    {
        controls.Player.Disable();
    }
}
