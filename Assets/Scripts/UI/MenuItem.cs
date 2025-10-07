using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FiveMinutesFarmer.UI
{
    public class MenuItem : MonoBehaviour
    {
        [SerializeField] private TMP_Text itemNameText;
        [SerializeField] private TMP_Text itemCountText;
        [SerializeField] private Button button;

        public void SetItem(string itemName, float value)
        {
            itemNameText.SetText(itemName);
            itemCountText.SetText(value.ToString());
        }
        public void SetOnClickAction(UnityEngine.Events.UnityAction action)
        {
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(action);
        }
    }
}