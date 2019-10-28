using UnityEngine.UI;

public abstract class UpgradedButtonController : ToggleButtonController
{
    public override void Initialize()
    {
        base.Initialize();

        gameObject.GetComponent<Button>().interactable = IsInteractable();

        ViewRender();
    }

    public abstract bool IsInteractable();
}
