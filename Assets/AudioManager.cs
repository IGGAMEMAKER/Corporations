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
        List<AudioSource> channels;
        Dictionary<AudioClip, AudioSource> sources;

        public AudioClip coinSound;
        int SOUND_COIN = 0;

        public AudioClip standardClickSound;
        int SOUND_STANDARD_CLICK = 1;

        public AudioClip notificationSound;
        int SOUND_NOTIFICATION = 2;

        public AudioClip toggleScreenSound;
        int SOUND_TOGGLE_SCREEN = 3;

        public AudioClip toggleButtonSound;
        int SOUND_TOGGLE_BUTTON = 4;

        void Start()
        {
            channels = new List<AudioSource>();
            sources = new Dictionary<AudioClip, AudioSource>();
            
            audioData = gameObject.AddComponent<AudioSource>();
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

            channels.Add(audioSource);
            sources[audioClip] = audioSource;
        }

        void PlaySound(int sound)
        {
            channels[sound].Play();
        }

        void Play(AudioClip clip)
        {
            //audioData.clip = clip;
            //audioData.Play();

            sources[clip].Play();
        }

        internal void PlayToggleButtonSound()
        {
            Play(toggleButtonSound);
        }

        internal void PlayOnHintHoverSound()
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
    }
}
