using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FiveMinutesFarmer.UI
{
    public class ShopItem : MonoBehaviour
    {
        [SerializeField] private Image itemIcon;
        [SerializeField] private TMP_Text itemNameText;
        [SerializeField] private TMP_Text itemCountText;
        [SerializeField] private Button button;

        public void SetItem(string itemName, int itemPrice, Sprite icon)
        {
            {
                itemNameText.SetText(itemName);
                itemCountText.SetText($"${itemPrice}");
                itemIcon.sprite = icon;
            }
        }

        public void SetButtonAction(UnityEngine.Events.UnityAction action)
        {
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(action);
        }
    }
}