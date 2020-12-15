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
        Upgrade,

        SignContract,
        CorporatePolicyTweak,
        Timer,

        ProgrammingTask,
        ServerTask,
        SupportTask,
        MarketingTask,

        ReapingPaper,
        FillPaper,

        GoalCompleted,
        GoalPicked,

        MoneySpent,
        
        Bubble1,
        Bubble2,
        Bubble3,
        Bubble4,
        Bubble5,
        Bubble6,
        Bubble7
    }

    public class AudioManager: MonoBehaviour
    {
        Dictionary<AudioClip, AudioSource> sources = new Dictionary<AudioClip, AudioSource>();
        Dictionary<Sound, AudioClip> sounds = new Dictionary<Sound, AudioClip>();

        public AudioClip coinSound;
        public AudioClip standardClickSound;
        public AudioClip notificationSound;
        public AudioClip toggleScreenSound;
        public AudioClip toggleButtonSound;
        public AudioClip hintSound;
        public AudioClip monthlyMoneySound;
        public AudioClip itemSelectedSound;
        public AudioClip penOnPaperSoung;
        public AudioClip leatherClickSound;
        public AudioClip timerSound;
        public AudioClip programmerTaskSound;
        public AudioClip marketingTaskSound;
        public AudioClip serverTaskSound;
        public AudioClip supportTaskSound;
        public AudioClip paperSound;
        public AudioClip reapingPpaperSound;
        public AudioClip goalCompletedSound;
        public AudioClip goalPickedSound;
        public AudioClip moneySpentSound;
        public AudioClip bubble1Sound;
        public AudioClip bubble2Sound;
        public AudioClip bubble3Sound;
        public AudioClip bubble4Sound;
        public AudioClip bubble5Sound;
        public AudioClip bubble6Sound;
        public AudioClip bubble7Sound;

        void Start()
        {
            AddSound(standardClickSound, Sound.StandardClick);
            AddSound(notificationSound, Sound.Notification);
            AddSound(leatherClickSound, Sound.Action);
            AddSound(hintSound, Sound.Hover);
            AddSound(monthlyMoneySound, Sound.MoneyIncome);
            AddSound(itemSelectedSound, Sound.Selected);

            AddSound(toggleScreenSound, Sound.Upgrade);
            AddSound(itemSelectedSound, Sound.Tweak);
            AddSound(penOnPaperSoung, Sound.SignContract);
            AddSound(itemSelectedSound, Sound.CorporatePolicyTweak);

            AddSound(timerSound, Sound.Timer);

            AddSound(programmerTaskSound, Sound.ProgrammingTask);
            AddSound(marketingTaskSound, Sound.MarketingTask);
            AddSound(serverTaskSound, Sound.ServerTask);
            AddSound(supportTaskSound, Sound.SupportTask);

            AddSound(paperSound, Sound.FillPaper);
            AddSound(reapingPpaperSound, Sound.ReapingPaper);

            AddSound(goalCompletedSound, Sound.GoalCompleted);
            AddSound(goalPickedSound, Sound.GoalPicked);

            AddSound(moneySpentSound, Sound.MoneySpent);
            
            // bubbles
            AddSound(bubble1Sound, Sound.Bubble1);
            AddSound(bubble2Sound, Sound.Bubble2);
            AddSound(bubble3Sound, Sound.Bubble3);
            AddSound(bubble4Sound, Sound.Bubble4);
            AddSound(bubble5Sound, Sound.Bubble5);
            AddSound(bubble6Sound, Sound.Bubble6);
            AddSound(bubble7Sound, Sound.Bubble7);
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

        public void PlayToggleButtonSound()
        {
            PlayClip(toggleButtonSound);
        }

        public void PlayOnHintHoverSound()
        {
            Play(Sound.Hover);
        }

        public void PlayCoinSound()
        {
            PlayClip(monthlyMoneySound);
        }

        public void PlayClickSound()
        {
            PlayClip(standardClickSound);
        }

        public void PlayPrepareAdSound()
        {
            PlayClip(standardClickSound);
        }

        public void PlayStartAdSound()
        {
            PlayClip(standardClickSound);
        }

        public void PlayNotificationSound()
        {
            PlayClip(notificationSound);
        }

        public void PlayToggleScreenSound()
        {
            PlayClip(toggleScreenSound);
        }

        public void PlayWaterSplashSound()
        {
            PlayClip(standardClickSound);
        }
    }
}
