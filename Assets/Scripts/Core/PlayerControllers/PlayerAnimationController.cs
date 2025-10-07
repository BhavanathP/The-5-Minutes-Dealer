using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimationController : MonoBehaviour
{
    private Animator animator;
    private Vector2 lastMoveDir = Vector2.down;

    private void Awake()
    {
        animator = GetComponent<Animator>();
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
        input.y = 0; // Lock movement to horizontal axis only
        if (input.sqrMagnitude > 0.1f)
        {
            lastMoveDir = input.normalized;
            animator.SetFloat("LastMoveX", lastMoveDir.x);
            animator.SetFloat("LastMoveY", lastMoveDir.y);
        }

        animator.SetFloat("MoveX", input.x);
        animator.SetFloat("MoveY", input.y);
        animator.SetFloat("Speed", input.sqrMagnitude);
    }
}
