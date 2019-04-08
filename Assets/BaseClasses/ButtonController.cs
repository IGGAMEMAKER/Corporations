using Assets.Utils;
using Entitas;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public abstract class ButtonController : MonoBehaviour, IEventGenerator
{
    public ProductComponent MyProduct;
    public GameEntity MyProductEntity;

    Button Button;

    public abstract void Execute();

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

    void Update()
    {
        UpdateControlledProductEntity();
    }

    void OnDestroy()
    {
        RemoveListener();
    }

    void UpdateControlledProductEntity()
    {
        MyProductEntity = GameContext.GetEntities(GameMatcher.AllOf(GameMatcher.Product, GameMatcher.ControlledByPlayer))[0];
        MyProduct = MyProductEntity.product;
    }

    void RemoveListener()
    {
        if (Button)
            Button.onClick.RemoveListener(Execute);
        else
            Debug.Log("This component is not assigned to Button. It is assigned to " + gameObject.name);
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
        MenuUtils.GetMenu(GameContext).ReplaceMenu(screenMode, data);
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
