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

        void Start()
        {
            audioData = gameObject.AddComponent<AudioSource>();
        }

        void Play(AudioClip clip)
        {
            audioData.clip = clip;
            audioData.Play();
            //audioData.loop = true;
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
    }
}
