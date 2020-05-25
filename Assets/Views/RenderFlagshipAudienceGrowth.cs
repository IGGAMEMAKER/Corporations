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

    void DetachListeners()
    {
        company.RemoveMarketingListener(this);
    }

    void OnDestroy()
    {
        DetachListeners();
    }
    void OnDisable()
    {
        DetachListeners();
    }

    public void SetEntity()
    {
        company = GetFollowableCompany();
        company.AddMarketingListener(this);

        previousClients = Marketing.GetClients(company);

        //Debug.Log("Attach to marketing changes: " + company.company.Name);
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

        previousClients = clients;

        //Debug.Log($"Marketing changes in {entity.company.Name}: +{change}");

        Render(change);
    }
}
