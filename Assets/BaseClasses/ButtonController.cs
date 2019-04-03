using Assets.Utils;
using Entitas;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public abstract class ButtonController : MonoBehaviour, IEventGenerator
{
    public GameContext GameContext;
    public ProductComponent ControlledProduct;
    public GameEntity ControlledProductEntity;

    Button Button;

    public abstract void Execute();

    void Awake()
    {
        GameContext = Contexts.sharedInstance.game;
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
        ControlledProductEntity = GameContext.GetEntities(GameMatcher.AllOf(GameMatcher.Product, GameMatcher.ControlledByPlayer))[0];
        ControlledProduct = ControlledProductEntity.product;
    }

    void RemoveListener()
    {
        Button.onClick.RemoveListener(Execute);
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

    public void SetSelectedNiche(NicheType niche)
    {
        MenuUtils.SetSelectedNiche(niche, GameContext);
    }

    public void TriggerEventUpgradeProduct(int productId, int ProductLevel)
    {
        ControlledProductEntity.AddEventUpgradeProduct(productId, ProductLevel);
    }

    public void TriggerEventTargetingToggle(int productId)
    {
        ControlledProductEntity.AddEventMarketingEnableTargeting(productId);
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
        int price = ControlledProductEntity.finance.price;

        ControlledProductEntity.AddEventFinancePricingChange(productId, price, change);
    }
}
