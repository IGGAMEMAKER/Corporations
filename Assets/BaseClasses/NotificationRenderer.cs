using UnityEngine;
using UnityEngine.UI;

public abstract class NotificationRenderer<T> : View where T : NotificationMessage
{
    //public abstract void Render(T message, Text Title, Text Description, GameObject LinkToEvent);
    public void Render(T message, Text Title, Text Description, GameObject LinkToEvent)
    {
        Description.text = GetDescription(message);
        Title.text = "NEWS: " + GetTitle(message);

        RemoveLinks();
        SetLink(message, LinkToEvent);
    }

    public abstract string GetTitle(T message);
    public abstract string GetDescription(T message);
    public abstract void SetLink(T message, GameObject LinkToEvent);

    public void RemoveLinks()
    {
        foreach (var l in GetComponents<ButtonController>())
            Destroy(l);
    }
}
