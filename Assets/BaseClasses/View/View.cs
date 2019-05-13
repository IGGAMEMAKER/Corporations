using Assets.Utils;
using Entitas;
using System;
using UnityEngine.UI;

public class View : BaseClass
{
    public float GetTaskCompletionPercentage(TaskComponent taskComponent)
    {
        return (CurrentIntDate - taskComponent.StartTime) * 100f / taskComponent.Duration;
    }

    public bool IsUnderPlayerControl(int companyId)
    {
        var c = CompanyUtils.GetCompanyById(GameContext, companyId);

        return c.isControlledByPlayer;
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

    public void Animate(Text text)
    {
        if (text.gameObject.GetComponent<TextBlink>() == null)
            text.gameObject.AddComponent<TextBlink>();
    }

    public void AnimateIfValueChanged(Text text, string value)
    {
        if (!String.Equals(text.text, value))
        {
            text.text = value;

            Animate(text);
        }
    }

    public void ListenMenuChanges(IMenuListener menuListener)
    {
        ScreenUtils.GetMenu(GameContext).AddMenuListener(menuListener);
    }

    public void ListenDateChanges(IAnyDateListener dateListener)
    {
        ScheduleUtils.ListenDateChanges(GameContext, dateListener);
    }
}
