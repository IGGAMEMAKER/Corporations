using Assets.Classes;
using Entitas;
using UnityEngine;
using UnityEngine.UI;

public class CompanyPreviewView : View, IEventListener, IProductListener
{
    GameEntity _entity;
    public Text Text;

    public void RegisterListeners(IEntity entity)
    {
        Debug.Log($"RegisterListeners CompanyPreviewView");

        _entity = (GameEntity)entity;
        _entity.AddProductListener(this);
    }

    void RenderCompanyName(string companyName)
    {
        Text.text = companyName;
    }

    void Update()
    {
        //GameEntity[] controlled = Contexts.sharedInstance.game
        //    .GetEntities(GameMatcher.AllOf(GameMatcher.Product, GameMatcher.ControlledByPlayer));

        //RenderCompanyName(controlled[0].product.Name);

        RenderCompanyName(myProduct.Name);
    }

    public void OnProduct(GameEntity entity, int id, string name, Niche niche, int productLevel, int explorationLevel, TeamResource resources)
    {
        Debug.Log($"OnProduct.");

        RenderCompanyName(name);
    }
}


