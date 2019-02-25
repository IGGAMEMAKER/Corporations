using System.Collections.Generic;
using Assets.Classes;
using Entitas;
using UnityEngine;
using UnityEngine.UI;

public class CompanyPreviewView : MonoBehaviour, IEventListener, IProductListener
{
    GameEntity _entity;
    public Text Text;

    public void RegisterListeners(IEntity entity)
    {
        Debug.Log($"RegisterListeners");
        _entity = (GameEntity)entity;
        _entity.AddProductListener(this);

        Text = GetComponent<Text>();
    }

    public void OnProduct(GameEntity entity, int id, string name, Niche niche, int productLevel, int explorationLevel, WorkerGroup team, TeamResource resources, int analytics, int experimentCount, uint clients, int brandPower, List<Advert> ads)
    {
        Debug.Log($"OnProduct");
        Text.text = name;
    }
}


