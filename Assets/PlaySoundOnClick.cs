using Assets;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class PlaySoundOnClick : MonoBehaviour
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
        SoundManager.Play(Sound);
    }

    void Destroy()
    {
        Button.onClick.RemoveListener(PlaySound);
    }
}
