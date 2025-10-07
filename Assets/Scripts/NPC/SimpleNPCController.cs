using UnityEngine;

public class SimpleNPCController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Animator animator;

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private Vector2 moveDurationRange = new Vector2(2f, 5f);
    [SerializeField] private Transform leftBoundary;
    [SerializeField] private Transform rightBoundary;

    [Header("Resting")]
    [SerializeField] private Vector2 restDurationRange = new Vector2(1f, 4f);

    private Rigidbody2D rb;
    private float timer;
    private bool isMoving;
    private float moveDirection;

    private float minX, maxX;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        if (leftBoundary == null || rightBoundary == null)
        {
            Debug.LogError("Boundary transforms are not assigned.", this);
            this.enabled = false;
            return;
        }

        minX = leftBoundary.position.x;
        maxX = rightBoundary.position.x;

        DecideNextAction();
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            DecideNextAction();
        }

        if (isMoving)
        {
            // Check if the NPC has reached or passed a boundary
            if ((transform.position.x <= minX && moveDirection < 0) || (transform.position.x >= maxX && moveDirection > 0))
            {
                // Clamp position to prevent going past the boundary
                float clampedX = Mathf.Clamp(transform.position.x, minX, maxX);
                transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);
                
                // Stop moving and start resting
                StartResting();
            }
        }
    }

    void FixedUpdate()
    {
        if (isMoving)
        {
            rb.linearVelocity = new Vector2(moveDirection * moveSpeed, rb.linearVelocity.y);
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
        }
    }

    void DecideNextAction()
    {
        if (isMoving)
        {
            StartResting();
        }
        else
        {
            StartMoving();
        }
    }

    void StartResting()
    {
        isMoving = false;
        timer = Random.Range(restDurationRange.x, restDurationRange.y);
        if (animator != null)
        {
            animator.Play("Idle");
        }
    }

    void StartMoving()
    {
        isMoving = true;
        moveDirection = Random.value > 0.5f ? 1f : -1f;
        timer = Random.Range(moveDurationRange.x, moveDurationRange.y);
        if (animator != null)
        {
            animator.Play("Move");
            animator.SetFloat("Direction", moveDirection);
        }
    }
}