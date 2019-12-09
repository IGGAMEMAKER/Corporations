using System.Collections.Generic;
using Assets.Utils;
using Entitas;

public class NotificationCheckNewCompaniesSystem : ReactiveSystem<GameEntity>
{
    public readonly Contexts contexts;
    public readonly GameContext gameContext;

    public NotificationCheckNewCompaniesSystem(Contexts contexts) : base(contexts.game)
    {
        this.contexts = contexts;
        gameContext = contexts.game;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var product in entities)
            NotificationUtils.AddNotification(gameContext, new NotificationMessageNewCompany(product.company.Id));
    }

    protected override bool Filter(GameEntity entity)
    {
        var player = Companies.GetPlayerCompany(gameContext);

        if (player == null)
            return false;

        return entity.hasProduct
            && entity.product.Concept == 0
            && Companies.IsInSphereOfInterest(player, entity);
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.Product.Added());
    }
}
