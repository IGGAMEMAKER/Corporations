using UnityEngine;
using UnityEngine.UI;

public class AnimatedText : MonoBehaviour
{
    public Animation Animation;
    public Text Text;

    public void Play(string text)
    {
        Text.text = text;
        Animation.Play();

        //var rand = Random.Range(-45, 45);
        //var angle = rand; // * Mathf.Deg2Rad;

        //transform.Rotate(0, 0, angle, Space.Self);
    }

    public void Update()
    {
        if (!Animation.isPlaying)
        {
            Destroy(gameObject);
        }
    }
}