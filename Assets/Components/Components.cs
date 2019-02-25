using Assets.Classes;
using Entitas;
using Entitas.CodeGeneration.Attributes;
using System.Collections.Generic;
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

//public interface IViewService
//{
//    // create a view from a premade asset (e.g. a prefab)
//    void LoadAsset(
//        Contexts contexts,
//        IEntity entity,
//        string assetName);
//}

[Game, Event(EventTarget.Self)]
public class PositionComponent : IComponent
{
    public float x;
    public float y;
}

public struct WorkerGroup
{
    public int Programmers;
    public int Managers;
    public int Marketers;
}

[Game, Event(EventTarget.Self)]
public class ProductComponent: IComponent
{
    public int Id;
    public string Name;
    public Niche Niche;

    public int ProductLevel;
    public int ExplorationLevel;

    public WorkerGroup Team;

    public TeamResource Resources;

    public int Analytics;
    public int ExperimentCount;

    public uint Clients;
    public int BrandPower;

    public List<Advert> Ads;
}




[Game]
public class DebugMessageComponent : IComponent
{
    public string message;
}
