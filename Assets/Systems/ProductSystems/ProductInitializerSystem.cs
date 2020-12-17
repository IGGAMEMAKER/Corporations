using Entitas;

public partial class ProductInitializerSystem : IInitializeSystem
{
    readonly GameContext GameContext;

    public ProductInitializerSystem(Contexts contexts)
    {
        GameContext = contexts.game;
    }

    void IInitializeSystem.Initialize()
    {
        Initialize();


    }

    void Initialize()
    {

    }
}


public partial class ProductInitializerSystem : IInitializeSystem
{

}
