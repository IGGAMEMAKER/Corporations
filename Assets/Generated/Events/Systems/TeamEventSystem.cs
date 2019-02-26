//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.EventSystemGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed class TeamEventSystem : Entitas.ReactiveSystem<GameEntity> {

    readonly System.Collections.Generic.List<ITeamListener> _listenerBuffer;

    public TeamEventSystem(Contexts contexts) : base(contexts.game) {
        _listenerBuffer = new System.Collections.Generic.List<ITeamListener>();
    }

    protected override Entitas.ICollector<GameEntity> GetTrigger(Entitas.IContext<GameEntity> context) {
        return Entitas.CollectorContextExtension.CreateCollector(
            context, Entitas.TriggerOnEventMatcherExtension.Added(GameMatcher.Team)
        );
    }

    protected override bool Filter(GameEntity entity) {
        return entity.hasTeam && entity.hasTeamListener;
    }

    protected override void Execute(System.Collections.Generic.List<GameEntity> entities) {
        foreach (var e in entities) {
            var component = e.team;
            _listenerBuffer.Clear();
            _listenerBuffer.AddRange(e.teamListener.value);
            foreach (var listener in _listenerBuffer) {
                listener.OnTeam(e, component.Programmers, component.Managers, component.Marketers, component.Morale);
            }
        }
    }
}
