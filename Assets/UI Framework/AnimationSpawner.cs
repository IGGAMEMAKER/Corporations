using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationSpawner : MonoBehaviour
{
    public AnimatedText AnimationPrefab;
    //List<AnimatedText> animationContainers = new List<AnimatedText>();

    //List<string> Texts = new List<string>();

    public void Spawn(string text)
    {
        Spawn(text, transform);
    }

    public void Spawn(string text, Transform t)
    {
        var anim = Instantiate(AnimationPrefab, t ?? transform, false);
        anim.Play(text);
    }
}
