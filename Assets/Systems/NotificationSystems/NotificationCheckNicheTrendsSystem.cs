using System.Collections.Generic;
using Assets.Utils;
using Entitas;

public class NotificationCheckNicheTrendsSystem : ReactiveSystem<GameEntity>
{
    public readonly Contexts contexts;
    public readonly GameContext gameContext;

    public NotificationCheckNicheTrendsSystem(Contexts contexts) : base(contexts.game)
    {
        this.contexts = contexts;
        gameContext = contexts.game;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var niche in entities)
            NotificationUtils.AddNotification(gameContext, new NotificationMessageTrendsChange(niche.niche.NicheType));
    }

    protected override bool Filter(GameEntity entity)
    {
        var player = CompanyUtils.GetPlayerCompany(gameContext);

        return entity.hasNicheState
            && entity.nicheState.Duration == 0
            && CompanyUtils.IsInSphereOfInterest(player, entity.niche.NicheType);
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.NicheState);
    }
}