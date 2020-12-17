using UnityEngine;

public class RenderCharacterScreenTabs : View
{
    public GameObject Ambitions;
    public GameObject Investments;

    public override void ViewRender()
    {
        base.ViewRender();

        Ambitions.SetActive(false && SelectedHuman.hasWorker);
        Investments.SetActive(SelectedHuman.hasShareholder);
    }
}
