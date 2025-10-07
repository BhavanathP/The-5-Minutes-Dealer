using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class UnityAudioPlayer : AudioPlayerBase
{
    private AudioSource source;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    public override void Play(AudioEntry entry, bool isSFX = false)
    {
        base.Play(entry, isSFX);

        if (isSFX)
            source.PlayOneShot(entry.clip, entry.volume);
        else
        {
            source.clip = entry.clip;
            source.volume = entry.volume;
            source.pitch = entry.pitch;
            source.loop = entry.loop;
            source.Play();
        }
    }

    public override void Stop()
    {
        source.Stop();
    }
}
