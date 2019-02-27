using Entitas;

public class UpgradeProductComponent : ButtonController
{
    public override void Execute()
    {
        ProductComponent product = GameContext.GetEntities(GameMatcher.AnyOf(GameMatcher.Product, GameMatcher.ControlledByPlayer))[0].product;

        AddEvent().AddEventUpgradeProduct(product.Id, product.ProductLevel);
    }

    // Start is called before the first frame update
    void Awake()
    {
        GameContext = Contexts.sharedInstance.game;
    }
}
