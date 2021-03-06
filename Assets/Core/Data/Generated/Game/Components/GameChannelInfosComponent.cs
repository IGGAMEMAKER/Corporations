//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public ChannelInfosComponent channelInfos { get { return (ChannelInfosComponent)GetComponent(GameComponentsLookup.ChannelInfos); } }
    public bool hasChannelInfos { get { return HasComponent(GameComponentsLookup.ChannelInfos); } }

    public void AddChannelInfos(System.Collections.Generic.Dictionary<int, ChannelInfo> newChannelInfos) {
        var index = GameComponentsLookup.ChannelInfos;
        var component = (ChannelInfosComponent)CreateComponent(index, typeof(ChannelInfosComponent));
        component.ChannelInfos = newChannelInfos;
        AddComponent(index, component);
    }

    public void ReplaceChannelInfos(System.Collections.Generic.Dictionary<int, ChannelInfo> newChannelInfos) {
        var index = GameComponentsLookup.ChannelInfos;
        var component = (ChannelInfosComponent)CreateComponent(index, typeof(ChannelInfosComponent));
        component.ChannelInfos = newChannelInfos;
        ReplaceComponent(index, component);
    }

    public void RemoveChannelInfos() {
        RemoveComponent(GameComponentsLookup.ChannelInfos);
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

    static Entitas.IMatcher<GameEntity> _matcherChannelInfos;

    public static Entitas.IMatcher<GameEntity> ChannelInfos {
        get {
            if (_matcherChannelInfos == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.ChannelInfos);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherChannelInfos = matcher;
            }

            return _matcherChannelInfos;
        }
    }
}
