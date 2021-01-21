using UnityEngine.UI;

public abstract class BlinkOnSomeCondition : View
{
    public void DestroyCompletely()
    {
        Destroy(gameObject);
    }

    public abstract bool ConditionCheck();

    Blinker Blinker
    {
        get
        {
            return gameObject.GetComponent<Blinker>();
        }
    }

    void SetButtonInteractable(bool interactable)
    {
        var b = GetComponent<Button>();

        if (b != null)
            b.interactable = interactable;
    }

    public override void ViewRender()
    {
        base.ViewRender();

        if (ConditionCheck())
        {
            if (Blinker == null)
            {
                gameObject.AddComponent<Blinker>();

                SetButtonInteractable(true);
            }
        }
        else
        {
            SetButtonInteractable(false);

            if (Blinker != null)
                Destroy(Blinker);
        }
    }
}
