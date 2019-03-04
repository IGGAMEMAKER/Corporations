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

    GameEntity[] GetProductsNotControlledByPlayer()
    {
        return GameContext
            .GetEntities(GameMatcher
                .AllOf(GameMatcher.Product)
                .NoneOf(GameMatcher.ControlledByPlayer)
                );
    }

    public GameEntity[] GetCompetitors()
    {
        GameEntity[] products = GetProductsNotControlledByPlayer();

        return Array.FindAll(products, e => e.product.Niche == myProduct.Niche);
    }

    public GameEntity[] GetNeighbours()
    {
        GameEntity[] products = GetProductsNotControlledByPlayer();

        return Array.FindAll(products, e => e.product.Niche != myProduct.Niche && e.product.Industry == myProduct.Industry);
    }


    GameEntity[] GetTasks(TaskType taskType)
    {
        // TODO: add filtering tasks, which are done by other players!

        GameEntity[] gameEntities = GameContext
            .GetEntities(GameMatcher.Task);

        return Array.FindAll(gameEntities, e => e.task.TaskType == taskType);
    }

    public TaskComponent GetTask(TaskType taskType)
    {
        GameEntity[] tasks = GetTasks(taskType);

        if (tasks.Length == 0)
            return null;

        return tasks[0].task;
    }

    public GameEntity myProductEntity
    {
        get
        {
            var products = GameContext
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
