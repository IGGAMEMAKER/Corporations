using UnityEngine;
using UnityEngine.UI;

public class BusinessScreenView : View
{
    public Text CompanyNameLabel;
    public LinkToProjectView LinkToProjectView;

    void Awake()
    {
        if (myProductEntity == null)
            Debug.Log("no companies controlled");
    }

    void Update()
    {
        Render();
    }

    void RenderSelectedCompanyName()
    {
        CompanyNameLabel.text = SelectedCompany.company.Name;
    }

    void SetLinkToProjectView()
    {
        LinkToProjectView.CompanyId = SelectedCompany.company.Id;
    }

    void Render()
    {
        SetLinkToProjectView();
        RenderSelectedCompanyName();
    }
}
