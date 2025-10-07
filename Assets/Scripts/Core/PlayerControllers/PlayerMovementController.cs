using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovementController : MonoBehaviour
{
    private bool audioPlaying = false;
    [SerializeField] private float moveSpeed = 5f;
    private Rigidbody2D rb;
    public Vector2 movementInput;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        if (InputManager.Instance != null)
            InputManager.Instance.OnMove += HandleMove;
    }

    private void OnDisable()
    {
        if (InputManager.Instance != null)
            InputManager.Instance.OnMove -= HandleMove;
    }

    private void HandleMove(Vector2 input)
    {
        movementInput = input.normalized;
        movementInput.y = 0; // Lock movement to horizontal axis only
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = movementInput * moveSpeed;
        if (movementInput != Vector2.zero && !audioPlaying)
        {
            audioPlaying = true;
            AudioManager.Instance.PlayMusic(AudioType.PlayerMove);
        }
        else if (movementInput == Vector2.zero && audioPlaying)
        {
            audioPlaying = false;
        }
    }
}
