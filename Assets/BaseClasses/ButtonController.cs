using Assets.Utils;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public abstract class ButtonController : MonoBehaviour, IEventGenerator
{
    Button Button;

    public abstract void Execute();

    public GameEntity SelectedCompany
    {
        get
        {
            var data = MenuUtils.GetMenu(GameContext).menu.Data;

            if (data == null)
            {
                //Debug.LogError("SelectedCompany does not exist!");

                return CompanyUtils.GetAnyOfControlledCompanies(GameContext);
            }

            return CompanyUtils.GetCompanyById(GameContext, (int)data);
        }
    }

    public ProductComponent MyProduct
    {
        get
        {
            return MyProductEntity?.product;
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

    void Start()
    {
        Button = GetComponent<Button>();

        Button.onClick.AddListener(Execute);
    }

    void OnDestroy()
    {
        RemoveListener();
    }

    void RemoveListener()
    {
        if (Button)
            Button.onClick.RemoveListener(Execute);
        else
            Debug.LogWarning("This component is not assigned to Button. It is assigned to " + gameObject.name);
    }

    public void NavigateToNiche(NicheType niche)
    {
        Navigate(ScreenMode.NicheScreen, niche);
    }

    public void NavigateToProjectScreen(int companyId)
    {
        Navigate(ScreenMode.ProjectScreen, companyId);
    }

    public void NavigateToIndustry(IndustryType industry)
    {
        Navigate(ScreenMode.IndustryScreen, industry);
    }

    public void NavigateToBusinessScreen(int companyId)
    {
        Navigate(ScreenMode.BusinessScreen, companyId);
    }

    public void Navigate(ScreenMode screenMode, object data)
    {
        MenuUtils.Navigate(GameContext, screenMode, data);
    }

    public void ReNavigate()
    {
        var m = MenuUtils.GetMenu(GameContext);

        m.ReplaceMenu(m.menu.ScreenMode, m.menu.Data);
    }


    public void SetSelectedCompany(int companyId)
    {
        MenuUtils.SetSelectedCompany(companyId, GameContext);
    }

    public void TriggerEventUpgradeProduct(int productId, int ProductLevel)
    {
        MyProductEntity.AddEventUpgradeProduct(productId, ProductLevel);
    }

    public void TriggerEventTargetingToggle(int productId)
    {
        MyProductEntity.AddEventMarketingEnableTargeting(productId);
    }

    public void TriggerEventIncreasePrice(int productId)
    {
        TriggerEventChangePrice(productId, 1);
    }

    public void TriggerEventDecreasePrice(int productId)
    {
        TriggerEventChangePrice(productId, -1);
    }

    void TriggerEventChangePrice(int productId, int change)
    {
        int price = MyProductEntity.finance.price;

        MyProductEntity.AddEventFinancePricingChange(productId, price, change);
    }
}
