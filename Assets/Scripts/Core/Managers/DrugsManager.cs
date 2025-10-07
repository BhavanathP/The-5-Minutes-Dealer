using System.Collections.Generic;
using UnityEngine;

public class DrugsManager : Singleton<DrugsManager>
{
    [SerializeField] private List<DrugData> drugs = new List<DrugData>();

    public DrugData GetDrugDetails(DrugType drugType)
    {
        return drugs.Find(c => c.drugType == drugType);
    }

    public List<DrugData> GetAllDrugs()
    {
        return drugs;
    }

    public Sprite GetDrugSprite(DrugType drugType)
    {
        var drug = GetDrugDetails(drugType);
        return drug != null ? drug.icon : null;
    }
}

[System.Serializable]
public class DrugData
{
    public DrugType drugType;
    public Sprite icon;
    public int buyPrice;
    public int sellPrice;
    public int duration;
}
