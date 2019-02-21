using System.Collections.Generic;
using UnityEngine;

namespace Assets.Classes
{
    public enum Niche
    {
        SocialNetwork,
        Messenger,
    }

    public class Project: MonoBehaviour
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

        public List<Advert> Ads;
    }
}
