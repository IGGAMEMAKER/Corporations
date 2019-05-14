using Assets.Utils;
using Entitas;
using System;
using UnityEngine;
using UnityEngine.UI;

public class BaseClass : MonoBehaviour
{
    public GameEntity SelectedCompany
    {
        get
        {
            return ScreenUtils.GetSelectedCompany(GameContext);
        }
    }

    public bool IsMyCompetitor
    {
        get
        {
            return SelectedCompany.hasProduct ? SelectedCompany.product.Niche == MyProduct.Niche : false;
        }
    }

    public ScreenMode CurrentScreen
    {
        get
        {
            return ScreenUtils.GetMenu(GameContext).menu.ScreenMode;
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
            return ScreenUtils.GetMenu(GameContext);
        }
    }
}
