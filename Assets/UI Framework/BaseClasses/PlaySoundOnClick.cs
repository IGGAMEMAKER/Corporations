using Assets;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class PlaySoundOnClick : View
{
    public Sound Sound;
    Button Button;

    void Start()
    {
        Button = GetComponent<Button>();

        Button.onClick.AddListener(PlaySound);
    }

    void PlaySound()
    {
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
