using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeamTaskView : View
{
    public int TeamId;
    public int SlotId;

    public Image Icon;

    public Sprite FeatureSprite;
    public Sprite ChannelSprite;

    public void SetEntity(int TeamId, int SlotId)
    {
        this.TeamId = TeamId;
        this.SlotId = SlotId;

        ViewRender();
    }

    public override void ViewRender()
    {
        base.ViewRender();

        var task = Flagship.team.Teams[TeamId].Tasks[SlotId];

        bool isFeature = task is TeamTaskFeatureUpgrade;
        bool isChannel = task is TeamTaskChannelActivity;

        // set image
        Icon.sprite = isFeature ? FeatureSprite : ChannelSprite;
    }
}
