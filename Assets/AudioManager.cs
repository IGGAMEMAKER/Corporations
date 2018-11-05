using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets
{
    public class AudioManager: MonoBehaviour
    {
        AudioSource audioData;

        public AudioClip coinSound;
        public AudioClip standardClickSound;
        public AudioClip notificationSound;
        public AudioClip toggleScreenSound;
        public AudioClip toggleButtonSound;

        void Start()
        {
            audioData = gameObject.AddComponent<AudioSource>();
        }

        internal void PlayToggleButtonSound()
        {
            Play(toggleButtonSound);
        }

        internal void PlayOnHintHoverSound()
        {
            Play(toggleButtonSound);
        }

        void Play(AudioClip clip)
        {
            audioData.clip = clip;
            audioData.Play();
        }

        public void PlayCoinSound()
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
    }
}
