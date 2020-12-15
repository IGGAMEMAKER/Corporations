using UnityEngine;

public class JumpingAnimation : MonoBehaviour
{
    public Animation Animation;

    public void Play()
    {
        Animation.Play();
    }

    public void Update()
    {
        if (!Animation.isPlaying)
        {
            Destroy(gameObject);
        }
    }
}