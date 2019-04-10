using UnityEngine.UI;

public class ProjectView : View, IMenuListener
{
    public Text CompanyName;
    public Text CompanyTypeLabel;

    void Start()
    {
        ListenMenuChanges(this);

        Render();
    }

    void Render()
    {
        CompanyName.text = SelectedCompany.company.Name;

        CompanyTypeLabel.text = SelectedCompany.company.CompanyType.ToString();
    }

    void IMenuListener.OnMenu(GameEntity entity, ScreenMode screenMode, object data)
    {
        if (screenMode == ScreenMode.ProjectScreen)
            Render();
    }
}
