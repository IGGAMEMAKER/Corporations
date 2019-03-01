using Entitas;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TaskType
{
    UpgradeProduct,
}

public class TaggedProgressBar : View
{
    public TaskType TaskType;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    TaskComponent GetTask(TaskType taskType)
    {
        GameEntity[] gameEntities = Contexts.sharedInstance.game
            .GetEntities(GameMatcher.AllOf(GameMatcher.Task));

        if (gameEntities.Length == 0)
            return null;

        return gameEntities[0].task;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
