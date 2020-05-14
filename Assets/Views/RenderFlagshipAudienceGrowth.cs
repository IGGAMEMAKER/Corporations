using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RenderFlagshipAudienceGrowth : BaseClass, IMarketingListener
{
    public Text Text;
    public Animation Animation;

    GameEntity company;

    long previousClients;

    void OnDestroy()
    {
        company.RemoveMarketingListener(this);
    }
    void OnDisable()
    {
        company.RemoveMarketingListener(this);
    }

    public void SetEntity(GameEntity company)
    {
        this.company = company;

        previousClients = Marketing.GetClients(company);

        company.AddMarketingListener(this);
    }

    void Render(long change)
    {
        if (Animation != null)
        {
            Animation.Play();

            Text.text = Format.Sign(change, true) + " users";
            Text.color = Visuals.GetColorPositiveOrNegative(change > 0);
        }
    }

    void IMarketingListener.OnMarketing(GameEntity entity, long clients)
    {
        var change = clients - previousClients;

        Render(change);

        previousClients = clients;
    }
}
