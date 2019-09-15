using Entitas;

public partial class CooldownContainerInitializerSystem : IInitializeSystem
{
    readonly GameContext GameContext;

    public CooldownContainerInitializerSystem(Contexts contexts)
    {
        GameContext = contexts.game;
    }

    void IInitializeSystem.Initialize()
    {
        var container = GameContext.CreateEntity();

        container.AddCooldownContainer(new System.Collections.Generic.Dictionary<string, Cooldown>());
    }
}
