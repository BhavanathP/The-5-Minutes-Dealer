using UnityEngine;
using System.Collections;
using FiveMinutesFarmer.UI;

// Assuming you have a PlayerController script like this:
// public class PlayerController : MonoBehaviour {
//     public bool isSelling = false;
// }

public class CopController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody2D rb;

    [Header("Patrol Behavior")]
    [SerializeField] private float walkSpeed = 2f;
    [SerializeField] private float runSpeed = 4f;
    [SerializeField] private float patrolRange = 5f;
    [SerializeField] private Vector2 restDurationRange = new Vector2(2f, 5f);
    [SerializeField] private GameObject turnIndicator; // Indicator to show before turning
    [SerializeField] private GameObject caughtIndicator; // Indicator to show when the player is caught

    [Header("Detection")]
    [SerializeField] private float detectionRange = 8f;
    [SerializeField] private float arrestDistance = 1f;
    private PlayerManager player => PlayerManager.Instance;

    private Vector3 initialPosition;
    private Vector3 minPatrolPoint;
    private Vector3 maxPatrolPoint;
    private Vector3 currentTarget;

    private bool isResting;
    private float restTimer;
    private bool isChasing;
    private float lastMoveDirection = 1f;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        turnIndicator.SetActive(false);
        caughtIndicator.SetActive(false);

        initialPosition = transform.position;
        minPatrolPoint = initialPosition - new Vector3(patrolRange, 0, 0);
        maxPatrolPoint = initialPosition + new Vector3(patrolRange, 0, 0);

        StartMoving(maxPatrolPoint);
    }

    void Update()
    {
        if (isChasing)
        {
            HandleChase();
        }
        else
        {
            HandlePatrol();
            CheckForPlayer();
        }
        animator.SetFloat("Direction", lastMoveDirection);
    }

    private void HandleChase()
    {
        if (player == null)
        {
            isChasing = false;
            StartResting(); // Or go back to patrol
            return;
        }

        currentTarget = player.transform.position;
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

        if (distanceToPlayer <= arrestDistance)
        {
            GameOver();
        }
    }

    private void HandlePatrol()
    {
        if (isResting)
        {
            restTimer -= Time.deltaTime;
            if (restTimer <= 0)
            {
                Vector3 newTarget = (Vector3.Distance(transform.position, maxPatrolPoint) < 0.2f) ? minPatrolPoint : maxPatrolPoint;
                StartMoving(newTarget);
            }
        }
        else
        {
            if (Vector3.Distance(transform.position, currentTarget) < 0.1f)
            {
                StartResting();
            }
        }
    }

    private void CheckForPlayer()
    {
        if (player != null && player.isSelling)
        {
            Vector2 directionToPlayer = (player.transform.position - transform.position).normalized;
            float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

            // The direction the cop is facing, as a vector
            Vector2 copForward = new Vector2(lastMoveDirection, 0);

            // The dot product will be > 0 if the player is in front of the cop,
            // < 0 if behind, and 0 if perfectly to the side.
            float dotProduct = Vector2.Dot(copForward, directionToPlayer);

            if (distanceToPlayer <= detectionRange && dotProduct > 0)
            {
                caughtIndicator.SetActive(true);
                StartChase();
            }
        }
    }

    void FixedUpdate()
    {
        if (isResting)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        if (Vector3.Distance(transform.position, currentTarget) > 0.1f)
        {
            float moveDirection = Mathf.Sign(currentTarget.x - transform.position.x);
            float currentSpeed = isChasing ? runSpeed : walkSpeed;
            rb.linearVelocity = new Vector2(moveDirection * currentSpeed, rb.linearVelocity.y);

            // Update the last move direction if moving
            if (moveDirection != 0)
            {
                lastMoveDirection = moveDirection;
            }
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
        }
    }

    void StartResting()
    {
        isResting = true;
        isChasing = false;
        restTimer = Random.Range(restDurationRange.x, restDurationRange.y);
        animator.Play("Idle");
        Invoke(nameof(EnableTurnIndicator), restTimer - 1f); // Show indicator 1 second before moving
    }

    void EnableTurnIndicator()
    {
        turnIndicator.SetActive(true);
    }

    void StartMoving(Vector3 newTarget)
    {
        turnIndicator.SetActive(false);
        isResting = false;
        isChasing = false;
        currentTarget = newTarget;
        animator.Play("Move");
    }

    void StartChase()
    {
        AudioManager.Instance.PlaySFX(AudioType.CopAlert);
        UIManager.Instance.isCaught = true;
        turnIndicator.SetActive(false);
        isChasing = true;
        isResting = false;
        if (animator != null)
        {
            animator.Play("Move"); // You might want a "Run" animation here
        }
    }

    void GameOver()
    {
        Debug.Log("GAME OVER - Player Caught!");
        isChasing = false;
        rb.linearVelocity = Vector2.zero;
        UIManager.Instance.ShowGameOver();
        // Here you would typically call a method on a GameManager to handle the game over sequence
        // For example: GameManager.Instance.EndGame();
        this.enabled = false; // Disable the cop controller
    }

    private void OnDrawGizmosSelected()
    {
        // Draw the patrol range
        Gizmos.color = Color.yellow;
        Vector3 startPos = Application.isPlaying ? initialPosition : transform.position;
        Gizmos.DrawLine(startPos - new Vector3(patrolRange, 0, 0), startPos + new Vector3(patrolRange, 0, 0));
        Gizmos.DrawWireSphere(startPos - new Vector3(patrolRange, 0, 0), 0.3f);
        Gizmos.DrawWireSphere(startPos + new Vector3(patrolRange, 0, 0), 0.3f);

        // Draw the detection range
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        // Draw the arrest range
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, arrestDistance);
    }
}
