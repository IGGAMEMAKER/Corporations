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

        public static void PlayFastCashSound()
        {
            Play(Sound.MoneyIncome);
        }

        public static void PlayOnHintHoverSound()
        {
            Play(Sound.Hover);
        }
    }
}
