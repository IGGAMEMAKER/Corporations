using UnityEngine.UI;

public class ProjectView : View
{
    public Text CompanyName;
    public Text CompanyDescription;

    void Update()
    {
        Render();
    }

    void Render()
    {
        CompanyName.text = SelectedCompany.company.Name;

        CompanyDescription.text = SelectedCompany.company.CompanyType.ToString();
    }
}
