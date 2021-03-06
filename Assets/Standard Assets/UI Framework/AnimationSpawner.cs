﻿using Assets;
using Assets.Core;
using UnityEngine;
using UnityEngine.UI;

public class AnimationSpawner : BaseClass
{
    public AnimatedText AnimationPrefab;
    public JumpingAnimation JumpingAnimationPrefab;

    private Sound[] bubbleSounds = new[] {Sound.Bubble1, Sound.Bubble2, Sound.Bubble3, Sound.Bubble4, Sound.Bubble5, Sound.Bubble6, Sound.Bubble7};

    public void SpawnJumpingAnimation(Transform t)
    {
        var anim = Instantiate(JumpingAnimationPrefab, transform, false);

        var angle = Random.Range(45, 135) * Mathf.Deg2Rad;
        
        anim.transform.Rotate(angle, angle, 0);

        PlaySound(RandomUtils.RandomItem(bubbleSounds));
        
        anim.Play();
    }

    public void Spawn(string text, Transform t)
    {
        var anim = Instantiate(AnimationPrefab, t ? t : transform, false);
        anim.Play(text, 0.15f);
    }

    public void Spawn(string text, Text txt, Transform t)
    {
        var anim = Instantiate(AnimationPrefab, t ? t : transform, false);
        anim.Play(text, 0.15f);
    }
}
