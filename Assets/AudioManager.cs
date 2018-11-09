using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets
{
    public class AudioManager: MonoBehaviour
    {
        Dictionary<AudioClip, AudioSource> sources;

        public AudioClip coinSound;
        public AudioClip standardClickSound;
        public AudioClip notificationSound;
        public AudioClip toggleScreenSound;
        public AudioClip toggleButtonSound;

        void Start()
        {
            sources = new Dictionary<AudioClip, AudioSource>();
            
            AddSound(coinSound);
            AddSound(standardClickSound);
            AddSound(notificationSound);
            AddSound(toggleScreenSound);
            AddSound(toggleButtonSound);
        }

        void AddSound(AudioClip audioClip)
        {
            AudioSource audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.clip = audioClip;

            sources[audioClip] = audioSource;
        }

        void Play(AudioClip clip)
        {
            sources[clip].Play();
        }

        internal void PlayToggleButtonSound()
        {
            Play(toggleButtonSound);
        }

        public void PlayOnHintHoverSound()
        {
            Play(toggleButtonSound);
        }

        internal void PlayCoinSound()
        {
            Play(coinSound);
        }

        internal void PlayClickSound()
        {
            Play(standardClickSound);
        }

        internal void PlayPrepareAdSound()
        {
            Play(standardClickSound);
        }

        internal void PlayStartAdSound()
        {
            Play(standardClickSound);
        }

        internal void PlayNotificationSound()
        {
            Play(notificationSound);
        }

        internal void PlayToggleScreenSound()
        {
            Play(toggleScreenSound);
        }

        internal void PlayWaterSplashSound()
        {
            Play(notificationSound);
        }
    }
}
