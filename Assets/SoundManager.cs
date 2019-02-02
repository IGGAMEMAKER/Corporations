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

        public void Play(Sound sound)
        {
            PickAudioManager();
            AudioManager.Play(sound);
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
