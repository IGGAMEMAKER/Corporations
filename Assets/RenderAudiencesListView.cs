using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AudienceInfo
{
    public string Name;
    public int Loyalty;
    public string Needs;
    public string Icon;
}

public class RenderAudiencesListView : ListView
{
    public override void SetItem<T>(Transform t, T entity, object data = null)
    {
        t.GetComponent<SegmentPreview>().SetEntity(entity as AudienceInfo);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        var audiences = new List<AudienceInfo>
        {
            new AudienceInfo { Loyalty = Random.Range(-15, 20), Name = "Teenagers",                 Needs = "Needs messaging, profiles, friends, voice chats, video chats, emojis, file sending", Icon = "Teenager" },
            new AudienceInfo { Loyalty = Random.Range(-15, 20), Name = "Adults (20-30 years)",      Needs = "Needs messaging, profiles, friends, voice chats", Icon = "Adult" },
            new AudienceInfo { Loyalty = Random.Range(-15, 20), Name = "Middle aged people (30+)",  Needs = "Needs messaging, profiles, friends, voice chats", Icon = "Middle" },
            new AudienceInfo { Loyalty = Random.Range(-15, 20), Name = "Old people",                Needs = "Needs messaging, friends, voice chats, video chats", Icon = "Old" },
        };

        SetItems(audiences);
    }

    private void OnEnable()
    {
        ViewRender();
    }
}
