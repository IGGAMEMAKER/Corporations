using Assets.Utils;
using Entitas;
using UnityEngine;

public class BaseClass : MonoBehaviour
{
    public GameEntity SelectedCompany
    {
        get
        {
            return ScreenUtils.GetSelectedCompany(GameContext);
        }
    }

    public bool IsMyProductCompany
    {
        get
        {
            return SelectedCompany.company.Id == MyProductEntity.company.Id;
        }
    }

    public bool IsMyCompetitor(GameEntity company)
    {
        bool isNotMyCompany = MyProductEntity.company.Id != company.company.Id;

        return company.hasProduct ? company.product.Niche == MyProduct.Niche && isNotMyCompany : false;
    }

    public ScreenMode CurrentScreen
    {
        get
        {
            return ScreenUtils.GetMenu(GameContext).menu.ScreenMode;
        }
    }

    public GameEntity Me
    {
        get
        {
            return GameContext.GetEntities(GameMatcher.Player)[0];
        }
    }

    public GameEntity SelectedHuman
    {
        get
        {
            return ScreenUtils.GetSelectedHuman(GameContext);
        }
    }

    public GameEntity MyProductEntity
    {
        get
        {
            return CompanyUtils.GetPlayerControlledProductCompany(GameContext);
        }
    }

    public bool HasProductCompany
    {
        get
        {
            return MyProductEntity != null;
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
            return ScreenUtils.GetMenu(GameContext);
        }
    }

    public bool Contains<T>()
    {
        return gameObject.GetComponent<T>() != null;
    }

    internal void ToggleIsChosenComponent(bool isChosen)
    {
        gameObject.GetComponent<IsChosenComponent>().Toggle(isChosen);
    }
}
