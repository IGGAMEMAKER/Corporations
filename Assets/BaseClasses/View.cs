using Assets.Utils;
using Entitas;
using System;
using UnityEngine;
using UnityEngine.UI;

public class View : MonoBehaviour
{
    public GameEntity SelectedCompany
    {
        get
        {
            var data = MenuUtils.GetMenu(GameContext).menu.Data;

            if (data == null)
            {
                Debug.LogError("SelectedCompany does not exist!");
                return null;
                //return CompanyUtils.GetAnyOfControlledCompanies(GameContext);
            }

            return CompanyUtils.GetCompanyById(GameContext, (int)data);
        }
    }

    public bool IsUnderPlayerControl(int companyId)
    {
        var c = CompanyUtils.GetCompanyById(GameContext, companyId);

        return c.isControlledByPlayer;
    }

    public ScreenMode CurrentScreen
    {
        get
        {
            return MenuUtils.GetMenu(GameContext).menu.ScreenMode;
        }
    }

    public GameEntity MyProductEntity
    {
        get
        {
            return CompanyUtils.GetPlayerControlledProductCompany(GameContext);
        }
    }

    public GameEntity MyGroupEntity
    {
        get
        {
            return CompanyUtils.GetPlayerControlledGroupCompany(GameContext);
        }
    }
    
    public GameContext GameContext
    {
        get
        {
            return Contexts.sharedInstance.game;
        }
    }

    public ProductComponent MyProduct
    {
        get
        {
            return MyProductEntity?.product;
        }
    }

    public int CurrentIntDate
    {
        get
        {
            return ScheduleUtils.GetCurrentDate(GameContext);
        }
    }

    public GameEntity GetUniversalListener
    {
        get
        {
            return MenuUtils.GetMenu(GameContext);
        }
    }

    public float GetTaskCompletionPercentage(TaskComponent taskComponent)
    {
        return (CurrentIntDate - taskComponent.StartTime) * 100f / taskComponent.Duration;
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
        MenuUtils.GetMenu(GameContext).AddMenuListener(menuListener);
    }

    public void ListenDateChanges(IAnyDateListener dateListener)
    {
        ScheduleUtils.ListenDateChanges(GameContext, dateListener);
    }
}
