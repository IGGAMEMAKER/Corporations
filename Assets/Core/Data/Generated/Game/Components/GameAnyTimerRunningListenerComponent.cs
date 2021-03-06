//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public AnyTimerRunningListenerComponent anyTimerRunningListener { get { return (AnyTimerRunningListenerComponent)GetComponent(GameComponentsLookup.AnyTimerRunningListener); } }
    public bool hasAnyTimerRunningListener { get { return HasComponent(GameComponentsLookup.AnyTimerRunningListener); } }

    public void AddAnyTimerRunningListener(System.Collections.Generic.List<IAnyTimerRunningListener> newValue) {
        var index = GameComponentsLookup.AnyTimerRunningListener;
        var component = (AnyTimerRunningListenerComponent)CreateComponent(index, typeof(AnyTimerRunningListenerComponent));
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceAnyTimerRunningListener(System.Collections.Generic.List<IAnyTimerRunningListener> newValue) {
        var index = GameComponentsLookup.AnyTimerRunningListener;
        var component = (AnyTimerRunningListenerComponent)CreateComponent(index, typeof(AnyTimerRunningListenerComponent));
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveAnyTimerRunningListener() {
        RemoveComponent(GameComponentsLookup.AnyTimerRunningListener);
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentMatcherApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class GameMatcher {

    static Entitas.IMatcher<GameEntity> _matcherAnyTimerRunningListener;

    public static Entitas.IMatcher<GameEntity> AnyTimerRunningListener {
        get {
            if (_matcherAnyTimerRunningListener == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.AnyTimerRunningListener);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherAnyTimerRunningListener = matcher;
            }

            return _matcherAnyTimerRunningListener;
        }
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.EventEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public void AddAnyTimerRunningListener(IAnyTimerRunningListener value) {
        var listeners = hasAnyTimerRunningListener
            ? anyTimerRunningListener.value
            : new System.Collections.Generic.List<IAnyTimerRunningListener>();
        listeners.Add(value);
        ReplaceAnyTimerRunningListener(listeners);
    }

    public void RemoveAnyTimerRunningListener(IAnyTimerRunningListener value, bool removeComponentWhenEmpty = true) {
        var listeners = anyTimerRunningListener.value;
        listeners.Remove(value);
        if (removeComponentWhenEmpty && listeners.Count == 0) {
            RemoveAnyTimerRunningListener();
        } else {
            ReplaceAnyTimerRunningListener(listeners);
        }
    }
}
