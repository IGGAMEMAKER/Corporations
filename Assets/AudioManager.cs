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
        Notification,
        Selected,

        Tweak,
        Upgrade
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
        public AudioClip hintSound;
        public AudioClip monthlyMoneySound;
        public AudioClip itemSelectedSound;

        void Start()
        {
            sources = new Dictionary<AudioClip, AudioSource>();
            sounds = new Dictionary<Sound, AudioClip>();
            
            AddSound(standardClickSound, Sound.StandardClick);
            AddSound(notificationSound, Sound.Notification);
            AddSound(toggleScreenSound, Sound.Action);
            AddSound(hintSound, Sound.Hover);
            AddSound(monthlyMoneySound, Sound.MoneyIncome);
            AddSound(itemSelectedSound, Sound.Selected);

            AddSound(toggleScreenSound, Sound.Upgrade);
            AddSound(itemSelectedSound, Sound.Tweak);
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
            if (sources.ContainsKey(clip))
                sources[clip].Play();
            else
                Debug.LogErrorFormat("Clip {0} doesn't exist in AudioManager", clip);
        }

        public void Play(Sound sound)
        {
            if (sounds.ContainsKey(sound))
                PlayClip(sounds[sound]);
            else
                Debug.LogErrorFormat("Sound {0} doesn't exist in AudioManager", sound);
        }

        internal void PlayToggleButtonSound()
        {
            PlayClip(toggleButtonSound);
        }

        public void PlayOnHintHoverSound()
        {
            Play(Sound.Hover);
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
