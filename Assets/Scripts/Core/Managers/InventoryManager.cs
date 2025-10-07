using System.Collections.Generic;
using UnityEngine;
using FiveMinutesFarmer.UI;

public class InventoryManager : Singleton<InventoryManager>
{
    [SerializeField] private List<Drugs> drugs = new List<Drugs>();
    private int waterCount;
    public int WaterCount => waterCount;

    private void Start()
    {
        waterCount = 2; // Starting water amount
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            UIManager.Instance.ToggleInventory();
        }
    }

    public void AddDrug(DrugType type, int value)
    {
        if (drugs.Exists(c => c.drugType == type))
        {
            var drug = drugs.Find(c => c.drugType == type);
            drug.count += value;
            return;
        }
        else
            drugs.Add(new Drugs { drugType = type, count = value });
    }
    public void UseDrug(DrugType type)
    {
        if (drugs.Exists(c => c.drugType == type))
        {
            var drug = drugs.Find(c => c.drugType == type);
            if (drug.count > 0)
                drug.count--;
            else
                Debug.LogWarning($"No {type} left in inventory!");
        }
        else
            Debug.LogWarning($"No {type} in inventory!");
    }

    public List<Drugs> GetAllDrugDetails()
    {
        return drugs;
    }
}

[System.Serializable]
public class Drugs
{
    public DrugType drugType;
    public int count;
}

[System.Serializable]
public enum DrugType
{
    None,
    Painkiller,
    Antibiotic,
    Antidepressant
}
