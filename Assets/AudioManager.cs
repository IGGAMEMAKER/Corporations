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

        void Start()
        {
            audioData = gameObject.AddComponent<AudioSource>();
            //coinSound = Resources.Load<AudioClip>("Sounds/Coin");
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
    }
}
