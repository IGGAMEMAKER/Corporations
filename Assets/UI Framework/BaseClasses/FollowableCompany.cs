using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowableCompany : MonoBehaviour
{
    public GameEntity Company;

    public void SetCompany(GameEntity company)
    {
        this.Company = company;

        // find listeners, who need followable company?
        var components = GetComponentsInChildren<FollowCompanyChanges>();

        if (components != null)
        {
            foreach (var c in components)
            {
                c.SetCompany(company);
            }
        }
    }
}
