using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace FiveMinutesFarmer.UI
{
    public class ShopManager : Singleton<ShopManager>
    {
        [SerializeField] private GameObject shopPanel;
        [SerializeField] private TMP_Text shopTitleText;
        [SerializeField] private Transform shopContentParent;
        [SerializeField] private GameObject shopItemPrefab;
        [SerializeField] private UnLockSystem unlockSystem;

        private void TryBuyDrug(DrugData drug)
        {
            AudioManager.Instance.PlaySFX(AudioType.UI_Click);
            if (CurrencyManager.Instance.SpendCurrency(drug.sellPrice))
            {
                InventoryManager.Instance.AddDrug(drug.drugType, 1);
                shopTitleText.SetText("Purchased " + drug.drugType.ToString());
                //unlockSystem.UnlockDrug(drug.drugType);
                Debug.Log($"{drug.drugType} purchased!");
            }
            else
            {
                Debug.Log("Not enough coins!");
            }
        }

        public void ToggleShop()
        {
            if (shopPanel.activeSelf)
                CloseShop();
            else
                OpenShop();
        }

        public void OpenShop()
        {
            shopTitleText.SetText("Shop Menu");
            InputManager.Instance.controls.Player.Pause.Disable();
            InputManager.Instance.controls.Player.Move.Disable();
            InputManager.Instance.OnBack += CloseShop;
            PopulateShop();
            shopPanel.SetActive(true);
        }

        public void CloseShop()
        {
            InputManager.Instance.OnBack -= CloseShop;
            shopPanel.SetActive(false);
            InputManager.Instance.controls.Player.Pause.Enable();
            InputManager.Instance.controls.Player.Move.Enable();
        }

        private void PopulateShop()
        {
            foreach (Transform child in shopContentParent)
            {
                Destroy(child.gameObject);
            }

            foreach (var drug in DrugsManager.Instance.GetAllDrugs())
            {
                if (!unlockSystem.IsDrugUnlocked(drug.drugType))
                {
                    var itemGO = Instantiate(shopItemPrefab, shopContentParent);
                    var shopItem = itemGO.GetComponent<ShopItem>();
                    shopItem.SetItem(drug.drugType.ToString(), drug.buyPrice, drug.icon);
                    shopItem.SetButtonAction(() => TryBuyDrug(drug));
                }
            }
        }
    }
}