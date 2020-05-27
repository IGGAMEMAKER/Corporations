//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public MarketingChannelComponent marketingChannel { get { return (MarketingChannelComponent)GetComponent(GameComponentsLookup.MarketingChannel); } }
    public bool hasMarketingChannel { get { return HasComponent(GameComponentsLookup.MarketingChannel); } }

    public void AddMarketingChannel(long newClients, ClientContainerType newContainerType, ChannelInfo newChannelInfo) {
        var index = GameComponentsLookup.MarketingChannel;
        var component = (MarketingChannelComponent)CreateComponent(index, typeof(MarketingChannelComponent));
        component.Clients = newClients;
        component.ContainerType = newContainerType;
        component.ChannelInfo = newChannelInfo;
        AddComponent(index, component);
    }

    public void ReplaceMarketingChannel(long newClients, ClientContainerType newContainerType, ChannelInfo newChannelInfo) {
        var index = GameComponentsLookup.MarketingChannel;
        var component = (MarketingChannelComponent)CreateComponent(index, typeof(MarketingChannelComponent));
        component.Clients = newClients;
        component.ContainerType = newContainerType;
        component.ChannelInfo = newChannelInfo;
        ReplaceComponent(index, component);
    }

    public void RemoveMarketingChannel() {
        RemoveComponent(GameComponentsLookup.MarketingChannel);
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

    static Entitas.IMatcher<GameEntity> _matcherMarketingChannel;

    public static Entitas.IMatcher<GameEntity> MarketingChannel {
        get {
            if (_matcherMarketingChannel == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.MarketingChannel);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherMarketingChannel = matcher;
            }

            return _matcherMarketingChannel;
        }
    }
}
