using UnityEngine;
using System.Collections.Generic;

public class VFXManager : Singleton<VFXManager>
{
    [SerializeField] private VFXDataBase vfxDatabase;
    [SerializeField] private VFXPlayerBase vfxPlayer;

    private Dictionary<VFXType, VFXEntry> vfxLookup;

    protected override void Awake()
    {
        base.Awake();
        vfxLookup = new Dictionary<VFXType, VFXEntry>();
        foreach (var entry in vfxDatabase.entries)
        {
            if (!vfxLookup.ContainsKey(entry.type))
                vfxLookup.Add(entry.type, entry);
        }
    }

    public void PlayVFX(VFXType type, Vector3 position)
    {
        if (vfxLookup.TryGetValue(type, out var entry))
            vfxPlayer?.Play(entry, position);
    }

    public void StopVFX(VFXType type)
    {

    }

    public void StopAllVFX()
    {
        vfxPlayer?.StopAll();
    }
}
