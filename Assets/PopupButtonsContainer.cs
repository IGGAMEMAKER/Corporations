using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupButtonsContainer : ListView
{
    public override void SetItem<T>(Transform t, T entity, object data = null)
    {
        AttachComponent(t, entity as System.Type);
    }

    void AttachComponent<T>(Transform t, T type) where T : System.Type
    {
        t.gameObject.AddComponent(type);
    }

    public void SetComponents(List<System.Type> components)
    {
        SetItems(components.ToArray());
    }
}
