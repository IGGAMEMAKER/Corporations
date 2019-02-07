using Assets;
using UnityEngine;
using UnityEngine.UI;

public class PlaySoundOnClick : MonoBehaviour
{
    public Sound Sound;
    Button Button;

    SoundManager SoundManager;
    
    void Start()
    {
        SoundManager = new SoundManager();

        Button = GetComponent<Button>();

        Button.onClick.AddListener(PlaySound);
    }

    void PlaySound()
    {
        SoundManager.Play(Sound);
    }

    void Destroy()
    {
        Button.onClick.RemoveListener(PlaySound);
    }
}
