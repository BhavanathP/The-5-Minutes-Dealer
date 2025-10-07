using UnityEngine;

[System.Serializable]
public class AudioEntry
{
    public AudioType type;
    public AudioClip clip;
    [Range(0f, 1f)] public float volume = 1f;
    [Range(0.5f, 2f)] public float pitch = 1f;
    public bool loop = false;
}

[CreateAssetMenu(menuName = "ScriptableObjects/Audio Database")]
public class AudioDatabase : ScriptableObject
{
    public AudioEntry[] entries;
}
