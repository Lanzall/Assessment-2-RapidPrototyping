using UnityEngine;

public static class AudioX
{
    static public void PlayRandomSound(AudioClip[] _clips, AudioSource _audioSource)
    {
        if (_clips.Length == 0)
            return;
        
        AudioClip clip = ArrayX.GetRandomItemFromArray(_clips);
        PlaySound(clip, _audioSource);
    }

    static public void PlayRandomSound(AudioClip[] _clips, AudioSource[] _audioSources, int _currentSoundPool)
    {

    }
    static public void PlaySound(AudioClip _clip, AudioSource _audioSource)
    {
        if (!_clip)
            return;
        
        _audioSource.clip = _clip;
        _audioSource.pitch = Random.Range(0.8f, 1.2f);
        _audioSource.Play();
    }
}
