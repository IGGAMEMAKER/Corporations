using Assets.Classes;
using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;

public enum Niche
{
    SocialNetwork,
    Messenger
}

public interface IEventListener
{
    void RegisterListeners(IEntity entity);
}

public interface IViewService
{
    // create a view from a premade asset (e.g. a prefab)
    void LoadAsset(
        Contexts contexts,
        IEntity entity,
        string assetName);
}

public interface IViewController
{
    Vector2 Position { get; set; }
    Vector2 Scale { get; set; }
    bool Active { get; set; }
    void InitializeView(Contexts contexts, IEntity Entity);
    void DestroyView();
}

public class UnityViewService : IViewService
{
    // now returns void instead of IViewController
    public void LoadAsset(Contexts contexts, IEntity entity, string assetName)
    {
        //Similar to before, but now we don't return anything. 
        var viewGo = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/" + assetName));
        if (viewGo != null)
        {
            var viewController = viewGo.GetComponent<IViewController>();
            if (viewController != null)
            {
                viewController.InitializeView(contexts, entity);
            }

            // except we add some lines to find and initialize any event listeners
            var eventListeners = viewGo.GetComponents<IEventListener>();
            foreach (var listener in eventListeners)
            {
                listener.RegisterListeners(entity);
            }
        }
    }
}

public struct IsPlayerControlled : IComponent
{
}

[Game, Event(EventTarget.Self)]
public class PositionComponent : IComponent
{
    public float x;
    public float y;
}

[Game, Event(EventTarget.Self)]
public class ProductComponent: IComponent
{
    public int Id;
    public string Name;
    public Niche Niche;

    public int ProductLevel;
    public int ExplorationLevel;

    public TeamResource Resources;
}

[Game, Event(EventTarget.Self)]
public class AnalyticsComponent: IComponent
{
    public int Analytics;
    public int ExperimentCount;
}

[Game, Event(EventTarget.Self)]
public class TeamComponent: IComponent
{
    public int Programmers;
    public int Managers;
    public int Marketers;

    public int Morale;
}

[Game, Event(EventTarget.Self)]
public class MarketingComponent : IComponent
{
    public uint Clients;
    public int BrandPower;
}




[Game]
public class DebugMessageComponent : IComponent
{
    public string message;
}
