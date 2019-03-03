using Entitas;
using System;
using UnityEngine;
using UnityEngine.UI;

public class View : MonoBehaviour
{
    public void AnimateIfValueChanged(Text text, string value)
    {
        if (!String.Equals(text.text, value))
        {
            text.text = value;
            text.gameObject.AddComponent<TextBlink>();
        }
    }

    public GameEntity myProductEntity
    {
        get
        {
            var products =  GameContext
                .GetEntities(GameMatcher.AllOf(GameMatcher.Product, GameMatcher.ControlledByPlayer));

            if (products.Length == 1) return products[0];

            return null;
        }
    }

    public GameContext GameContext
    {
        get
        {
            return Contexts.sharedInstance.game;
        }
    }

    public ProductComponent myProduct {
        get
        {
            return myProductEntity == null ? null : myProductEntity.product;
        }
    }

    public int CurrentIntDate
    {
        get
        {
            return GameContext
                .GetEntities(GameMatcher.Date)[0].date.Date;
        }
    }
}
