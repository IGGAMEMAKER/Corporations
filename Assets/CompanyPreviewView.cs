using Assets.Classes;
using Entitas;
using UnityEngine;
using UnityEngine.UI;

public class CompanyPreviewView : MonoBehaviour, IEventListener, IProductListener
{
    GameEntity _entity;
    public Text Text;

    public void Start()
    {
        Text = GetComponent<Text>();

    }

    public void RegisterListeners(IEntity entity)
    {
        Debug.Log($"RegisterListeners");
        _entity = (GameEntity)entity;
        _entity.AddProductListener(this);
    }

    void Update()
    {
        GameEntity[] entities = Contexts.sharedInstance.game.GetEntities(GameMatcher.AllOf(GameMatcher.Product, GameMatcher.ControlledByPlayer));

        Text.text = entities[0].product.Name;
    }

    public void OnProduct(GameEntity entity, int id, string name, Niche niche, int productLevel, int explorationLevel, TeamResource resources)
    {
        Debug.Log($"OnProduct");
        Text.text = name;
    }
}


