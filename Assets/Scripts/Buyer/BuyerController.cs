using Player;
using UnityEngine;
using UnityEngine.UI;

public class BuyerController : MonoBehaviour, IInteractable
{
    public string _currentStateName = "Idle";
    [Header("Components")]
    public Animator m_animator;
    public Rigidbody2D m_rigidbody2D;
    public Collider2D triggerCollider;

    [Header("State Machine")]
    public StateMachine stateMachine;
    public BuyerIdleState idleState;
    public BuyerMoveState moveState;
    public BuyingState buyingState;
    public WaitingState waitingState;
    public BuyerReturnState returnState;

    [Header("Movement & Targeting")]
    public float MoveSpeed = 2f;
    public Transform targetPoint;

    [Header("Timers & Behavior")]
    public float waitTime = 2f;
    public float buyingTime = 0f;
    public float buyingTimer = 0f;

    [Header("UI & Interaction")]
    public GameObject drugImageObject;
    public Image drugImage;
    public DrugType drugType;
    public GameObject timerImage;
    public Image timerFillImage;
    public Transform buyingPoint;

    void Awake()
    {
        m_animator = GetComponent<Animator>();
        m_rigidbody2D = GetComponent<Rigidbody2D>();
        triggerCollider = GetComponent<Collider2D>();

        stateMachine = new StateMachine();
        idleState = new BuyerIdleState(this);
        moveState = new BuyerMoveState(this);
        buyingState = new BuyingState(this);
        waitingState = new WaitingState(this);
        returnState = new BuyerReturnState(this);
    }

    private void OnEnable()
    {
        drugType = DrugType.None;
        stateMachine.TransitionTo(idleState);
        buyingTimer = 0;
    }

    private void Update()
    {
        _currentStateName = stateMachine._currentState.GetType().Name;
        stateMachine.Update();

        if (buyingTimer > 0f && buyingTimer < buyingTime)
        {
            timerImage.SetActive(true);
            timerFillImage.fillAmount = buyingTimer / buyingTime;
        }
        else
        {
            timerImage.SetActive(false);
        }

        triggerCollider.enabled = stateMachine._currentState == buyingState || stateMachine._currentState == waitingState;
    }

    private void FixedUpdate()
    {
        stateMachine.FixedUpdate();
    }

    void IInteractable.Interact()
    {
        Debug.Log("Interacting with Buyer");
        if (stateMachine._currentState == waitingState)
        {
            stateMachine.TransitionTo(buyingState);
        }
    }
    void IInteractable.StopInteract()
    {
        Debug.Log("Stopped Interacting with Buyer");
        if (stateMachine._currentState == buyingState)
        {
            stateMachine.TransitionTo(waitingState);
        }
    }
}
