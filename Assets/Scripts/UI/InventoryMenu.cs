using TMPro;
using UnityEngine;

namespace FiveMinutesFarmer.UI
{
    public class InventoryMenu : MonoBehaviour
    {
        public GameObject inventoryPanel;
        [SerializeField] private InventoryItem itemPrefab;
        [SerializeField] private Transform contentParent;
        public Sprite coinIcon;

        public void Show()
        {
            InputManager.Instance.controls.Player.Pause.Disable();
            InputManager.Instance.controls.Player.Move.Disable();
            InputManager.Instance.OnBack += Hide;
            PopulateInventory();
            inventoryPanel.SetActive(true);
        }
        public void Hide()
        {
            InputManager.Instance.OnBack -= Hide;
            InputManager.Instance.controls.Player.Pause.Enable();
            InputManager.Instance.controls.Player.Move.Enable();
            inventoryPanel.SetActive(false);
        }

        private void PopulateInventory()
        {
            foreach (Transform child in contentParent)
            {
                Destroy(child.gameObject);
            }

            //Crops
            foreach (var drug in InventoryManager.Instance.GetAllDrugDetails())
            {
                if (drug.count <= 0) continue;
                var itemGO = Instantiate(itemPrefab, contentParent);
                var icon = DrugsManager.Instance.GetDrugSprite(drug.drugType);
                itemGO.SetItem(icon, drug.drugType.ToString(), drug.count);
            }
        }
    }
}