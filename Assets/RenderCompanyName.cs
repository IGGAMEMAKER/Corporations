using UnityEngine.UI;

public class RenderCompanyName : View
{
    void OnEnable()
    {
        GetComponent<Text>().text = SelectedCompany.company.Name;
    }
}
