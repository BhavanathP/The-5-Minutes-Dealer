using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{
    public PlayerMovementController Movement { get; private set; }
    public PlayerInteractionController Interaction { get; private set; }
    public PlayerAnimationController Animation { get; private set; }
    public bool isSelling = false;
    public GameObject successFX;

    protected override void Awake()
    {
        base.Awake();

        Movement = GetComponent<PlayerMovementController>();
        Interaction = GetComponent<PlayerInteractionController>();
        Animation = GetComponent<PlayerAnimationController>();
    }

    public void ShowSuccessEffect()
    {
        if (successFX != null)
        {
            successFX.SetActive(false); // Reset in case it's already active
            successFX.SetActive(true);
            Invoke(nameof(DisableFX), 1.5f); // Disable after 1.5 seconds
        }
    }

    private void DisableFX() => successFX.SetActive(false);
}
