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

    void RenderShareholders(Dictionary<int, int> shareholders, GameObject Container)
    {
        int index = 0;

        int totalShares = 0;

        foreach (var e in shareholders)
        {
            totalShares += e.Value;
        }

        foreach (var e in shareholders)
        {
            Transform c;

            Debug.Log("Shareholder: " + e.Key + " - " + e.Value);

            if (index < Container.transform.childCount)
            {
                c = Container.transform.GetChild(index);
            }
            else
            {
                c = Instantiate(ShareholderPreviewPrefab, Container.transform, false).transform;
            }

            c.gameObject.GetComponent<ShareholderPreviewView>().SetEntity(e.Key, e.Value, totalShares);
            index++;
        }
    }
}
