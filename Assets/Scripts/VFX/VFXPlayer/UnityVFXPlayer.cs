using UnityEngine;

public class UnityVFXPlayer : VFXPlayerBase
{
    protected override void OnPlay(GameObject instance, VFXEntry entry)
    {
        // If it has a ParticleSystem, restart it
        var ps = instance.GetComponent<ParticleSystem>();
        if (ps != null)
        {
            ps.Clear();
            ps.Play();
        }

        // If it has animation, you could restart here too
    }
}
