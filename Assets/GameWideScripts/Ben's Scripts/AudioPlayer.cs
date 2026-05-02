using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [Header("Audio")]
    public AudioSource audioSource;
    public Vector2 pitchRange = new Vector2(0.95f, 1.05f);

    [System.Serializable]
    public class AudioGroup
    {
        public string groupName;
        public AudioClip[] clips;
    }

    public AudioGroup[] audioGroups;

    [System.Serializable]
    public class NamedClip
    {
        public string name;
        public AudioClip clip;
    }

    public NamedClip[] namedClips;

    // Play a specific named clip
    public void PlayClip(string clipName)
    {
        foreach (var namedClip in namedClips)
        {
            if (namedClip.name == clipName)
            {
                audioSource.PlayOneShot(namedClip.clip);
                return;
            }
        }
        Debug.LogWarning($"AudioPlayer: No clip found with name '{clipName}'");
    }

    // Play a random clip from a named group
    public void PlayRandomFromGroup(string groupName)
    {
        foreach (var group in audioGroups)
        {
            if (group.groupName == groupName)
            {
                audioSource.pitch = Random.Range(pitchRange.x, pitchRange.y);
                PlayRandomFromArray(group.clips);
                audioSource.pitch = 1f;
                return;
            }
        }

        Debug.LogWarning($"AudioPlayer: No audio group found with name '{groupName}'");
    }

    // Play a specific clip from a named group
    public void PlayClipFromGroup(string groupName, int index)
    {
        foreach (var group in audioGroups)
        {
            if (group.groupName == groupName)
            {
                PlaySpecificClip(group.clips, index);
                return;
            }
        }

        Debug.LogWarning($"AudioPlayer: No audio group found with name '{groupName}'");
    }

    // Helpers
    private void PlayRandomFromArray(AudioClip[] clips)
    {
        if (clips == null || clips.Length == 0) return;

        audioSource.pitch = Random.Range(pitchRange.x, pitchRange.y);
        audioSource.PlayOneShot(clips[Random.Range(0, clips.Length)]);
        audioSource.pitch = 1f;
    }

    private void PlaySpecificClip(AudioClip[] clips, int index)
    {
        if (clips == null || clips.Length == 0) return;
        if (index < 0 || index >= clips.Length) return;

        audioSource.pitch = Random.Range(pitchRange.x, pitchRange.y);
        audioSource.PlayOneShot(clips[index]);
        audioSource.pitch = 1f;
    }
}