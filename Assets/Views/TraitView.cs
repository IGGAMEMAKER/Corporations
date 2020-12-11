using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TraitView : View
{
    public Sprite Ambitious;
    public Sprite Careerist;
    public Sprite Curious;
    public Sprite Executor;
    public Sprite Greedy;
    public Sprite Independent;
    public Sprite Leadership;
    public Sprite Challenger;
    public Sprite Shy;
    public Sprite Stable;
    public Sprite Teacher;
    public Sprite Useful;
    public Sprite Visionaire;
    public Sprite Creative;

    public Image Image;
    public Hint Hint;

    public Trait Trait;

    public void SetEntity(Trait trait)
    {
        base.ViewRender();

        RenderTraitType(trait);

        Hint.SetHint(trait.ToString());
    }

    private void OnValidate()
    {
        RenderTraitType(Trait);
    }

    public void RenderTraitType(Trait trait)
    {
        switch (trait)
        {
            case Trait.Ambitious:
                Image.sprite = Ambitious;
                break;

            case Trait.Careerist:
                Image.sprite = Careerist;
                break;

            case Trait.Curious:
                Image.sprite = Curious;
                break;

            case Trait.Executor:
                Image.sprite = Executor;
                break;

            case Trait.Greedy:
                Image.sprite = Greedy;
                break;

            case Trait.Independence:
                Image.sprite = Independent;
                break;

            case Trait.Leader:
                Image.sprite = Leadership;
                break;

            case Trait.NewChallenges:
                Image.sprite = Challenger;
                break;

            case Trait.Shy:
                Image.sprite = Shy;
                break;

            case Trait.Loyal:
                Image.sprite = Stable;
                break;

            case Trait.Teacher:
                Image.sprite = Teacher;
                break;

            case Trait.Useful:
                Image.sprite = Useful;
                break;

            case Trait.Visionaire:
                Image.sprite = Visionaire;
                break;

            case Trait.WantsToCreate:
                Image.sprite = Creative;
                break;

            default:
                break;
        }
    }
}
