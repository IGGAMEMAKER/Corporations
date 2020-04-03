using UnityEngine;

namespace Assets
{
    public static class SoundManager
    {
        public static void Play(Sound sound)
        {
            AudioManager AudioManager = GameObject.FindObjectOfType<AudioManager>();

            AudioManager.Play(sound);
        }

        public static void PlayOnHintHoverSound()
        {
            Play(Sound.Hover);
        }
    }
}
