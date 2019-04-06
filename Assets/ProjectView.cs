using UnityEngine.UI;

public class ProjectView : View, IMenuListener
{
    public Text CompanyName;
    public Text CompanyTypeLabel;
    public LinkToCompanyPreview LinkToCompanyPreview;

    void Start()
    {
        ListenMenuChanges(this);

        Render();
    }

    void SetLinkToCompanyPreview()
    {
        LinkToCompanyPreview.CompanyId = SelectedCompany.company.Id;
    }

    void Render()
    {
        CompanyName.text = SelectedCompany.company.Name;

        CompanyTypeLabel.text = SelectedCompany.company.CompanyType.ToString();

        SetLinkToCompanyPreview();
    }

    void IMenuListener.OnMenu(GameEntity entity, ScreenMode screenMode, object data)
    {
        if (screenMode == ScreenMode.ProjectScreen)
            Render();
    }
}
