//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public MarketingListenerComponent marketingListener { get { return (MarketingListenerComponent)GetComponent(GameComponentsLookup.MarketingListener); } }
    public bool hasMarketingListener { get { return HasComponent(GameComponentsLookup.MarketingListener); } }

    public void AddMarketingListener(System.Collections.Generic.List<IMarketingListener> newValue) {
        var index = GameComponentsLookup.MarketingListener;
        var component = (MarketingListenerComponent)CreateComponent(index, typeof(MarketingListenerComponent));
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceMarketingListener(System.Collections.Generic.List<IMarketingListener> newValue) {
        var index = GameComponentsLookup.MarketingListener;
        var component = (MarketingListenerComponent)CreateComponent(index, typeof(MarketingListenerComponent));
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveMarketingListener() {
        RemoveComponent(GameComponentsLookup.MarketingListener);
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

    static Entitas.IMatcher<GameEntity> _matcherMarketingListener;

    public static Entitas.IMatcher<GameEntity> MarketingListener {
        get {
            if (_matcherMarketingListener == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.MarketingListener);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherMarketingListener = matcher;
            }

            return _matcherMarketingListener;
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

    public void AddMarketingListener(IMarketingListener value) {
        var listeners = hasMarketingListener
            ? marketingListener.value
            : new System.Collections.Generic.List<IMarketingListener>();
        listeners.Add(value);
        ReplaceMarketingListener(listeners);
    }

    public void RemoveMarketingListener(IMarketingListener value, bool removeComponentWhenEmpty = true) {
        var listeners = marketingListener.value;
        listeners.Remove(value);
        if (removeComponentWhenEmpty && listeners.Count == 0) {
            RemoveMarketingListener();
        } else {
            ReplaceMarketingListener(listeners);
        }
    }
}