using UnityEngine;
using System.Collections.Generic;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] private AudioDatabase audioDatabase;
    [SerializeField] private AudioPlayerBase sfxPlayer;
    [SerializeField] private AudioPlayerBase musicPlayer;

    private Dictionary<AudioType, AudioEntry> audioLookup;

    protected override void Awake()
    {
        base.Awake();

        // Build lookup dictionary
        audioLookup = new Dictionary<AudioType, AudioEntry>();
        foreach (var entry in audioDatabase.entries)
        {
            if (!audioLookup.ContainsKey(entry.type))
                audioLookup.Add(entry.type, entry);
        }
    }

    public void PlaySFX(AudioType type)
    {
        if (audioLookup.TryGetValue(type, out var entry))
            sfxPlayer?.Play(entry, true);
    }

    public void PlayMusic(AudioType type)
    {
        if (audioLookup.TryGetValue(type, out var entry))
        {
            StopMusic();
            musicPlayer?.Play(entry);
        }
    }

    public void StopMusic() => musicPlayer?.Stop();
}
