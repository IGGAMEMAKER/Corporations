using System.Collections.Generic;
using UnityEngine;

namespace Assets
{
    public enum Sound
    {
        None,
        Hover,
        Action,
        MoneyIncome,
        StandardClick,
        Notification
    }

    public class AudioManager: MonoBehaviour
    {
        Dictionary<AudioClip, AudioSource> sources;
        Dictionary<Sound, AudioClip> sounds;

        public AudioClip coinSound;
        public AudioClip standardClickSound;
        public AudioClip notificationSound;
        public AudioClip toggleScreenSound;
        public AudioClip toggleButtonSound;
        public AudioClip monthlyMoneySound;

        void Start()
        {
            sources = new Dictionary<AudioClip, AudioSource>();
            sounds = new Dictionary<Sound, AudioClip>();
            
            AddSound(monthlyMoneySound, Sound.MoneyIncome);
            AddSound(standardClickSound, Sound.StandardClick);
            AddSound(notificationSound, Sound.Notification);
            AddSound(toggleScreenSound, Sound.Action);
            AddSound(toggleButtonSound, Sound.Hover);
        }

        void AddSound(AudioClip audioClip, Sound sound = Sound.None)
        {
            AudioSource audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.clip = audioClip;

            sources[audioClip] = audioSource;
            sounds[sound] = audioClip;
        }

        void PlayClip(AudioClip clip)
        {
            sources[clip].Play();
        }

        public void Play(Sound sound)
        {
            if (sounds.ContainsKey(sound))
                PlayClip(sounds[sound]);
            else
                Debug.LogFormat("Sound {0} doesn't exist in AudioManager", sound);
        }

        internal void PlayToggleButtonSound()
        {
            PlayClip(toggleButtonSound);
        }

        public void PlayOnHintHoverSound()
        {
            PlayClip(toggleButtonSound);
        }

        internal void PlayCoinSound()
        {
            PlayClip(monthlyMoneySound);
        }

        internal void PlayClickSound()
        {
            PlayClip(standardClickSound);
        }

        internal void PlayPrepareAdSound()
        {
            PlayClip(standardClickSound);
        }

        internal void PlayStartAdSound()
        {
            PlayClip(standardClickSound);
        }

        internal void PlayNotificationSound()
        {
            PlayClip(notificationSound);
        }

        internal void PlayToggleScreenSound()
        {
            PlayClip(toggleScreenSound);
        }

        internal void PlayWaterSplashSound()
        {
            PlayClip(standardClickSound);
        }
    }
}
