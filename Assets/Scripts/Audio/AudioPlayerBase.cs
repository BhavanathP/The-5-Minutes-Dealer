using UnityEngine;

public interface IAudioPlayer
{
    void Play(AudioEntry entry, bool isSFX = false);
    void Stop();
}

public abstract class AudioPlayerBase : MonoBehaviour, IAudioPlayer
{
    protected AudioEntry currentEntry;

    public virtual void Play(AudioEntry entry, bool isSFX = false)
    {
        currentEntry = entry;
        // Derived class decides how to use it (AudioSource, FMOD event, etc.)
    }

    public abstract void Stop();
}
