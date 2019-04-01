using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProjectView : View
{
    public Text CompanyName;
    public Text CompanyDescription;
    public LinkToCompanyPreview LinkToCompanyPreview;
    public GameObject ShareholderPreviewPrefab;
    public GameObject ShareholderContainer;

    void Update()
    {
        Render();
    }

    void Render()
    {
        CompanyName.text = SelectedCompany.company.Name;

        CompanyDescription.text = SelectedCompany.company.CompanyType.ToString();

        LinkToCompanyPreview.CompanyId = SelectedCompany.company.Id;

        Dictionary<int, int> shareholders = new Dictionary<int, int>();

        if (SelectedCompany.hasShareholders)
            shareholders = SelectedCompany.shareholders.Shareholders;

        RenderShareholders(shareholders, ShareholderContainer);
    }

    int GetTotalShares(Dictionary<int, int> shareholders)
    {
        int totalShares = 0;

        foreach (var e in shareholders)
            totalShares += e.Value;

        return totalShares;
    }

    void RemoveInstances(int amount)
    {
        // Remove useless gameobjects
    }

    void SpawnInstances(int amount, GameObject Container)
    {
        for (var i = 0; i < amount; i++)
            Instantiate(ShareholderPreviewPrefab, Container.transform, false);
    }

    void ProvideEnoughInstances(Dictionary<int, int> shareholders, GameObject Container)
    {
        int childCount = Container.transform.childCount;

        if (shareholders.Count < childCount)
            RemoveInstances(childCount - shareholders.Count);
        else
            SpawnInstances(shareholders.Count - childCount, Container);
    }

    void RenderShareholders(Dictionary<int, int> shareholders, GameObject Container)
    {
        int index = 0;

        int totalShares = GetTotalShares(shareholders);

        ProvideEnoughInstances(shareholders, Container);

        foreach (var e in shareholders)
        {
            Container.transform.GetChild(index)
                .gameObject
                .GetComponent<ShareholderPreviewView>()
                .SetEntity(e.Key, e.Value, totalShares);
            index++;
        }
    }
}
