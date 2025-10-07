using UnityEngine;
using System.Collections.Generic;

public interface IVFXPlayer
{
    void Play(VFXEntry entry, Vector3 position);
    void StopAll();
}

public abstract class VFXPlayerBase : MonoBehaviour, IVFXPlayer
{
    protected class VFXPool
    {
        public Queue<GameObject> objects = new Queue<GameObject>();
        public Transform parent;
    }

    protected Dictionary<VFXType, VFXPool> pools = new Dictionary<VFXType, VFXPool>();

    public virtual void Play(VFXEntry entry, Vector3 position)
    {
        if (!pools.ContainsKey(entry.type))
            CreatePool(entry);

        var pool = pools[entry.type];
        var instance = GetFromPool(entry, pool);

        instance.transform.position = position;
        instance.SetActive(true);

        // Let child decide how to actually play it
        OnPlay(instance, entry);

        if (entry.duration > 0)
            StartCoroutine(ReturnToPoolAfter(entry, instance, pool));
    }

    public void StopAll()
    {
        foreach (var pool in pools.Values)
        {
            foreach (var obj in pool.objects)
                obj.SetActive(false);
        }
    }

    protected abstract void OnPlay(GameObject instance, VFXEntry entry);

    private void CreatePool(VFXEntry entry)
    {
        var pool = new VFXPool();
        pool.parent = new GameObject($"{entry.type}_Pool").transform;
        pool.parent.SetParent(transform);

        for (int i = 0; i < entry.initialPoolSize; i++)
        {
            var obj = Instantiate(entry.prefab, pool.parent);
            obj.SetActive(false);
            pool.objects.Enqueue(obj);
        }

        pools[entry.type] = pool;
    }

    private GameObject GetFromPool(VFXEntry entry, VFXPool pool)
    {
        if (pool.objects.Count > 0)
            return pool.objects.Dequeue();

        var obj = Instantiate(entry.prefab, pool.parent);
        obj.SetActive(false);
        return obj;
    }

    private System.Collections.IEnumerator ReturnToPoolAfter(VFXEntry entry, GameObject instance, VFXPool pool)
    {
        yield return new WaitForSeconds(entry.duration);
        instance.SetActive(false);
        pool.objects.Enqueue(instance);
    }
}
