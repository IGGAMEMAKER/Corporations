using Assets;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class PlaySoundOnClick : View
{
    public Sound Sound;
    Button Button;

    public AudioClip AudioClip;

    void Start()
    {
        Button = GetComponent<Button>();

        Button.onClick.AddListener(PlaySound);
    }

    void PlaySound()
    {
        //if (AudioClip != null)
        //{
            
        //    SoundManager.Play(AudioSource);
        //    return;
        //}

        if (Sound == Sound.None)
        {
            Debug.LogWarning("no sound specified at " + CurrentScreen.ToString() + " " + gameObject.name);
            return;
        }

        SoundManager.Play(Sound);
    }

    void Destroy()
    {
        Button.onClick.RemoveListener(PlaySound);
    }
}
