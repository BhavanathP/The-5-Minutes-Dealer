using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FiveMinutesFarmer.UI
{
    public class InventoryItem : MonoBehaviour
    {
        [SerializeField] private Image itemIcon;
        [SerializeField] private TMP_Text itemNameText;
        [SerializeField] private TMP_Text itemCountText;

        public void SetItem(Sprite icon, string itemName, int itemCount)
        {
            itemIcon.sprite = icon;
            itemNameText.SetText(itemName);
            itemCountText.SetText($"X{itemCount}");
        }
    }
}