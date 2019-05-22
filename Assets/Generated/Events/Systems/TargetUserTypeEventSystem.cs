//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.EventSystemGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed class TargetUserTypeEventSystem : Entitas.ReactiveSystem<GameEntity> {

    readonly System.Collections.Generic.List<ITargetUserTypeListener> _listenerBuffer;

    public TargetUserTypeEventSystem(Contexts contexts) : base(contexts.game) {
        _listenerBuffer = new System.Collections.Generic.List<ITargetUserTypeListener>();
    }

    protected override Entitas.ICollector<GameEntity> GetTrigger(Entitas.IContext<GameEntity> context) {
        return Entitas.CollectorContextExtension.CreateCollector(
            context, Entitas.TriggerOnEventMatcherExtension.Added(GameMatcher.TargetUserType)
        );
    }

    protected override bool Filter(GameEntity entity) {
        return entity.hasTargetUserType && entity.hasTargetUserTypeListener;
    }

    protected override void Execute(System.Collections.Generic.List<GameEntity> entities) {
        foreach (var e in entities) {
            var component = e.targetUserType;
            _listenerBuffer.Clear();
            _listenerBuffer.AddRange(e.targetUserTypeListener.value);
            foreach (var listener in _listenerBuffer) {
                listener.OnTargetUserType(e, component.UserType);
            }
        }
    }
}
