using UnityEngine;

namespace Plugins.AltoCityUIPack.Scripts
{
    public static class AudioAltoCityUIPack
    {
        private static AudioSource _audioSource;

        public static void SetAudioSource(AudioSource audioSource)
        {
            _audioSource = audioSource;
        }
        
        public static void PlayUISound(this AudioClip clip)
        {
            if (!_audioSource || !clip)
            {
                return;
            }
        
            _audioSource.volume = Random.Range(0.95f, 1.0f);
            _audioSource.pitch = Random.Range(0.95f, 1.05f);
            _audioSource.PlayOneShot(clip);
        }
    }
}
