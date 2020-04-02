using UnityEngine.UI;

public class RenderGroupName : View
{
    public override void ViewRender()
    {
        base.ViewRender();

        GetComponent<Text>().text = MyCompany.company.Name;
    }
}

