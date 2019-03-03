using Entitas;
using UnityEngine;

public class View : MonoBehaviour
{
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
