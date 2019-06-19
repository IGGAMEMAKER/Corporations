//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public SegmentComponent segment { get { return (SegmentComponent)GetComponent(GameComponentsLookup.Segment); } }
    public bool hasSegment { get { return HasComponent(GameComponentsLookup.Segment); } }

    public void AddSegment(System.Collections.Generic.Dictionary<UserType, int> newSegments) {
        var index = GameComponentsLookup.Segment;
        var component = (SegmentComponent)CreateComponent(index, typeof(SegmentComponent));
        component.Segments = newSegments;
        AddComponent(index, component);
    }

    public void ReplaceSegment(System.Collections.Generic.Dictionary<UserType, int> newSegments) {
        var index = GameComponentsLookup.Segment;
        var component = (SegmentComponent)CreateComponent(index, typeof(SegmentComponent));
        component.Segments = newSegments;
        ReplaceComponent(index, component);
    }

    public void RemoveSegment() {
        RemoveComponent(GameComponentsLookup.Segment);
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

    static Entitas.IMatcher<GameEntity> _matcherSegment;

    public static Entitas.IMatcher<GameEntity> Segment {
        get {
            if (_matcherSegment == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.Segment);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherSegment = matcher;
            }

            return _matcherSegment;
        }
    }
}
