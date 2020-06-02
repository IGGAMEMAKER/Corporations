using System.Collections.Generic;
using Assets.Core;

public class ExploreChannelsSystem : OnDateChange
{
    public ExploreChannelsSystem(Contexts contexts) : base(contexts) { }

    protected override void Execute(List<GameEntity> entities)
    {
        var flagship = Companies.GetPlayerFlagship(gameContext);

        if (flagship == null)
            return;

        List<int> progressing = new List<int>();

        foreach (var k in flagship.channelExploration.InProgress.Keys)
            progressing.Add(k);

        foreach (var channelId in progressing)
        {
            if (flagship.channelExploration.InProgress[channelId] > 0)
            {
                flagship.channelExploration.InProgress[channelId]--;
            }
            else
            {
                flagship.channelExploration.Explored.Add(channelId);
                flagship.channelExploration.InProgress.Remove(channelId);
            }
        }
    }
}

