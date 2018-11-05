using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets
{
    public class SoundManager : MonoBehaviour
    {
        AudioManager AudioManager;

        void PickAudioManager()
        {
            AudioManager = GameObject.Find("Core").GetComponent<AudioManager>();
        }

        public void PlayToggleSound()
        {
            PickAudioManager();
            AudioManager.PlayToggleButtonSound();
        }

        public void PlayOnHintHoverSound()
        {
            PickAudioManager();
            AudioManager.PlayOnHintHoverSound();
        }
    }
}
