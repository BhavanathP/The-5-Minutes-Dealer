using UnityEngine;
using FiveMinutesFarmer.UI;

public class ShopInteractable : MonoBehaviour, IInteractable
{
    void IInteractable.Interact()
    {
        ShopManager.Instance.ToggleShop();
    }
    void IInteractable.StopInteract()
    {
        // Optional: Implement if needed
    }
}
