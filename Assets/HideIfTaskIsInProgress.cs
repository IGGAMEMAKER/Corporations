using Entitas;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Hides start action buttons for player convenience
public class HideIfTaskIsInProgress : MonoBehaviour
{
    public TaskType TaskType;
    public GameObject GameObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    bool HasTask(TaskType taskType)
    {
        GameEntity[] gameEntities = Contexts.sharedInstance.game.GetEntities(GameMatcher.Task);
        // TODO: add filtering tasks, which are done by other players!

        return gameEntities.Length > 0;
    }

    // Update is called once per frame
    void Update()
    {
        GameObject.SetActive(HasTask(TaskType));
    }
}
