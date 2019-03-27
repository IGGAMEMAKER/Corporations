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

    private void Awake()
    {
        GameContext = Contexts.sharedInstance.game;
    }

    private void Update()
    {
        ControlledProductEntity = GameContext.GetEntities(GameMatcher.AllOf(GameMatcher.Product, GameMatcher.ControlledByPlayer))[0];
        ControlledProduct = ControlledProductEntity.product;
    }

    //public GameEntity StartTask()
    //{
    //    return ControlledProductEntity;
    //}

    //public GameEntity SendEvent()
    //{
    //    // you can attach events to this object
    //    return GameContext.CreateEntity();
    //}

    void Start()
    {
        Button = GetComponent<Button>();

        Button.onClick.AddListener(Execute);
    }

    private void OnDestroy()
    {
        Button.onClick.RemoveListener(Execute);
    }

    public void Navigate(ScreenMode screenMode)
    {
        MenuUtils.Menu(GameContext).ReplaceMenu(screenMode);
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
