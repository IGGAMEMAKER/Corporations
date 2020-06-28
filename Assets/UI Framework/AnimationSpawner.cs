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
        //Texts.Add(text);

        var anim = Instantiate(AnimationPrefab, transform, false);
        anim.Play(text);
    }
}
