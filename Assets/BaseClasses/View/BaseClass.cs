using Assets.Utils;
using Entitas;
using UnityEngine;
using UnityEngine.UI;

public class BaseClass : MonoBehaviour
{
    public GameEntity SelectedCompany => ScreenUtils.GetSelectedCompany(GameContext);

    public bool IsMyProductCompany
    {
        get
        {
            if (!HasProductCompany)
                return false;

            return SelectedCompany.company.Id == MyProductEntity.company.Id;
        }
    }

    public NicheType SelectedNiche => ScreenUtils.GetSelectedNiche(GameContext);

    public bool IsMyCompetitor(GameEntity company)
    {
        if (!HasProductCompany)
            return false;

        bool isNotMyCompany = MyProductEntity.company.Id != company.company.Id;

        return company.hasProduct ? company.product.Niche == MyProduct.Niche && isNotMyCompany : false;
    }

    public bool IsMyCompetitor(int companyId)
    {
        var company = CompanyUtils.GetCompanyById(GameContext, companyId);

        return IsMyCompetitor(company);
    }

    public ScreenMode CurrentScreen => ScreenUtils.GetMenu(GameContext).menu.ScreenMode;

    public GameEntity Me => GameContext.GetEntities(GameMatcher.Player)[0];

    public GameEntity SelectedHuman => ScreenUtils.GetSelectedHuman(GameContext);

    public GameEntity MyProductEntity => CompanyUtils.GetPlayerControlledProductCompany(GameContext);

    public bool HasCompany => MyCompany != null;

    public GameEntity MyCompany
    {
        get
        {
            if (MyProductEntity != null)
                return MyProductEntity;

            if (MyGroupEntity != null)
                return MyGroupEntity;

            return null;
        }
    }

    public long Balance => MyCompany.companyResource.Resources.money;

    public bool HasProductCompany => MyProductEntity != null;
    public bool HasGroupCompany => MyGroupEntity != null;

    public GameEntity MyGroupEntity => CompanyUtils.GetPlayerControlledGroupCompany(GameContext);
    public ProductComponent MyProduct => MyProductEntity?.product;

    public static GameContext GameContext => Contexts.sharedInstance.game;

    public int CurrentIntDate => ScheduleUtils.GetCurrentDate(GameContext);
    public int CurrentIntYear => Constants.START_YEAR + CurrentIntDate / 360;

    public GameEntity GetUniversalListener => ScreenUtils.GetMenu(GameContext);



    public bool Contains<T>()
    {
        return gameObject.GetComponent<T>() != null;
    }

    public T AddIfAbsent<T>() where T : Component
    {
        if (!Contains<T>())
            return gameObject.AddComponent<T>();

        return gameObject.GetComponent<T>();
    }

    public void RemoveIfExists<T>() where T : Component
    {
        if (Contains<T>())
            Destroy(gameObject.GetComponent<T>());
    }

    public void AddOrRemove<T>() where T : Component
    {
        if (Contains<T>())
            Destroy(gameObject.GetComponent<T>());
    }



    internal void ToggleIsChosenComponent(bool isChosen)
    {
        gameObject.GetComponent<IsChosenComponent>().Toggle(isChosen);
    }
}
