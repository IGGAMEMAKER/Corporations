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
        Debug.Log($"RegisterListeners CompanyPreviewView");
        _entity = (GameEntity)entity;
        _entity.AddProductListener(this);
    }

    void Update()
    {
        GameEntity[] controlled = Contexts.sharedInstance.game.GetEntities(GameMatcher.AllOf(GameMatcher.Product, GameMatcher.ControlledByPlayer));
        GameEntity[] entities = Contexts.sharedInstance.game.GetEntities(GameMatcher.AllOf(GameMatcher.Product));

        //Contexts.sharedInstance.game.CreateEntity().AddEvent(new UpgradeFeatureEvent(projId));

        Text.text = controlled[0].product.Name;

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            controlled[0].isControlledByPlayer = false;
            entities[3].isControlledByPlayer = true;
        }
    }

    public void OnProduct(GameEntity entity, int id, string name, Niche niche, int productLevel, int explorationLevel, TeamResource resources)
    {
        Debug.Log($"OnProduct");
        Text.text = name;
    }
}


