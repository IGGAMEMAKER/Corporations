using Entitas;
using UnityEngine;
using UnityEngine.UI;

public interface IEventGenerator
{
    void TriggerEventUpgradeProduct(int productId, int ProductLevel);
    void TriggerEventUpgradeAnalytics(int productId);
    void TriggerEventTargetingToggle(int productId);
}

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
}
