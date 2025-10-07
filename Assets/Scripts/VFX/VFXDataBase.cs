using UnityEngine;

[System.Serializable]
public class VFXEntry
{
    public VFXType type;
    public GameObject prefab;
    public float duration = 2f;
    public int initialPoolSize = 5;
}

[CreateAssetMenu(menuName = "ScriptableObjects/VFX Database")]
public class VFXDataBase : ScriptableObject
{
    public VFXEntry[] entries;
}
