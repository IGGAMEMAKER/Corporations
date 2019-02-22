using Assets.Classes;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public enum Niche
{
    SocialNetwork,
    Messenger
}

public class Product : MonoBehaviour
{
    public int Id;
    public string Name;
    public Niche Niche;

    public int Level;
    public int ExplorationLevel;

    public uint Clients;

    public int Programmers;
    public int Managers;
    public int Marketers;

    public TeamResource Resources;

    public int Analytics;
    public int ExperimentCount;

    public int BrandPower;

    public List<Advert> Ads;
}

public struct Product2 : ISharedComponentData
{
    public int Id;
    public string Name;
    public Niche Niche;

    public int Level;
    public int ExplorationLevel;

    public uint Clients;

    public int Programmers;
    public int Managers;
    public int Marketers;

    public TeamResource Resources;

    public int Analytics;
    public int ExperimentCount;

    public int BrandPower;

    public List<Advert> Ads;
}
