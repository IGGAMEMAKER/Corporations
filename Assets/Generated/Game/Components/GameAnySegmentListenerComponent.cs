//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public AnySegmentListenerComponent anySegmentListener { get { return (AnySegmentListenerComponent)GetComponent(GameComponentsLookup.AnySegmentListener); } }
    public bool hasAnySegmentListener { get { return HasComponent(GameComponentsLookup.AnySegmentListener); } }

    public void AddAnySegmentListener(System.Collections.Generic.List<IAnySegmentListener> newValue) {
        var index = GameComponentsLookup.AnySegmentListener;
        var component = (AnySegmentListenerComponent)CreateComponent(index, typeof(AnySegmentListenerComponent));
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceAnySegmentListener(System.Collections.Generic.List<IAnySegmentListener> newValue) {
        var index = GameComponentsLookup.AnySegmentListener;
        var component = (AnySegmentListenerComponent)CreateComponent(index, typeof(AnySegmentListenerComponent));
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveAnySegmentListener() {
        RemoveComponent(GameComponentsLookup.AnySegmentListener);
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

    static Entitas.IMatcher<GameEntity> _matcherAnySegmentListener;

    public static Entitas.IMatcher<GameEntity> AnySegmentListener {
        get {
            if (_matcherAnySegmentListener == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.AnySegmentListener);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherAnySegmentListener = matcher;
            }

            return _matcherAnySegmentListener;
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

    public void AddAnySegmentListener(IAnySegmentListener value) {
        var listeners = hasAnySegmentListener
            ? anySegmentListener.value
            : new System.Collections.Generic.List<IAnySegmentListener>();
        listeners.Add(value);
        ReplaceAnySegmentListener(listeners);
    }

    public void RemoveAnySegmentListener(IAnySegmentListener value, bool removeComponentWhenEmpty = true) {
        var listeners = anySegmentListener.value;
        listeners.Remove(value);
        if (removeComponentWhenEmpty && listeners.Count == 0) {
            RemoveAnySegmentListener();
        } else {
            ReplaceAnySegmentListener(listeners);
        }
    }
}
