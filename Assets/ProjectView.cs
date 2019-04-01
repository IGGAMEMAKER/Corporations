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

        RenderShareholders(SelectedCompany.shareholders.Shareholders, ShareholderContainer);
    }

    private ShareholdersComponent GetShareholders()
    {
        return SelectedCompany.shareholders;
    }

    void RenderShareholders(Dictionary<int, int> shareholders, GameObject Container)
    {
        int index = 0;

        int totalShares = 0;

        foreach (var e in shareholders)
        {
            totalShares += e.Value;
        }

        //Debug.Log("")

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
                c.SetSiblingIndex(index);
            }

            //c.gameObject.GetComponent<ShareholderPreviewView>().SetEntity(e.Key, e.Value, totalShares);
            index++;
        }
    }
}
