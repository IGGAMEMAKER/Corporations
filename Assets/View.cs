using Entitas;
using UnityEngine;

public class View : MonoBehaviour
{
    public GameEntity myProductEntity
    {
        get
        {
            return GameContext
                .GetEntities(GameMatcher.AllOf(GameMatcher.Product, GameMatcher.ControlledByPlayer))[0];
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
            return myProductEntity.product;
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
