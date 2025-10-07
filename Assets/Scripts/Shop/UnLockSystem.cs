using System.Collections.Generic;
using UnityEngine;

public class UnLockSystem : MonoBehaviour
{
    private HashSet<DrugType> unlockedCrops = new HashSet<DrugType>();

    public void UnlockCrop(DrugType _DrugType)
    {
        unlockedCrops.Add(_DrugType);
    }

    public bool IsDrugUnlocked(DrugType _DrugType)
    {
        return unlockedCrops.Contains(_DrugType);
    }
}
